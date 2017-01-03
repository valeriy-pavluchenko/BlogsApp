using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BlogsApp.Api.Exceptions;
using BlogsApp.Api.Extensions;
using BlogsApp.Api.Filters;
using BlogsApp.Api.Middleware;
using BlogsApp.Api.Models;
using BlogsApp.DataAccess.Exceptions;
using BlogsApp.DataAccess.Repositories;
using BlogsApp.DataAccess.Tools;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Swashbuckle.Swagger.Model;

namespace BlogsApp.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        /// <summary>
        /// Services Configuration
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(config =>
                {
                    //config.Filters.Add(typeof(CustomAuthorizationFilter));
                    //config.Filters.Add(typeof (CustomExceptionFilter));
                    config.ModelValidatorProviders.Clear();
                })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<Startup>();

                    Func<Type, MemberInfo, LambdaExpression, string> nameResolver = (type, info, expression) =>
                    {
                        var dataContract = (DataContractAttribute)info?.GetCustomAttribute(typeof(DataContractAttribute));
                        var dataMember = (DataMemberAttribute)info?.GetCustomAttribute(typeof(DataMemberAttribute));

                        return dataMember?.Name ?? dataContract?.Name;
                    };

                    ValidatorOptions.PropertyNameResolver = nameResolver;
                    ValidatorOptions.DisplayNameResolver = nameResolver;
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.None;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            services.AddSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "BlogsApp API",
                    Description = "Some ASP.NET Core Investigation",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Valeriy Pavluchenko", Email = "valeriy.pavluchenko@gmail.com", Url = "https://github.com/valeriy-pavluchenko" }
                });
                options.DescribeAllEnumsAsStrings();
                options.IncludeXmlComments(AppContext.BaseDirectory + "\\BlogsApp.Api.xml");
            });

            // Find this solution but currently not working
            services.Configure<JwtBearerOptions>(options =>
            {
                var validator = options.SecurityTokenValidators.OfType<JwtSecurityTokenHandler>().SingleOrDefault();

                // Turn off Microsoft's JWT handler that maps claim types to .NET's long claim type names
                validator.InboundClaimTypeMap = new Dictionary<string, string>();
                validator.OutboundClaimTypeMap = new Dictionary<string, string>();
            });

            services.AddSingleton<IConfigurationRoot>(_configuration);
            services.AddSingleton<IRepositories>(x => new Repositories(_configuration.GetConnectionString("BlogsAppApi")));
            UpdateDatabaseToLatestVersion();
        }

        /// <summary>
        /// Application configuration
        /// </summary>
        /// <param name="app">Application builder</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlingMiddleware>();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUi();

            //app.UseDeveloperExceptionPage();

            #region Use JWT Tokens

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetAppSettings("SecretKey")));
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                
                ValidateIssuer = true,
                ValidIssuer = _configuration.GetAppSettings("Issuer"),

                ValidateAudience = true,
                ValidAudience = _configuration.GetAppSettings("Audience"),

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            var options = new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters,
                AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
                Events = new JwtBearerEvents { OnTokenValidated = OnTokenValidated }
            };

            app.UseJwtBearerAuthentication(options);
            //app.UseMiddleware<CustomAuthenticationMiddleware>(); - same logic is proposed in OnTokenValidated method that overrides default JwtBearerEvents

            #endregion

            app.UseMvc();
        }

        private Task OnTokenValidated(TokenValidatedContext context)
        {
            var repositories = context.HttpContext.RequestServices.GetService<IRepositories>();

            if (!context.Ticket.Principal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
            {
                throw new TokenValidationFailedException("Invalid authorization ticket content.");
            }

            var nameIdentifierClaim = context.Ticket.Principal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            var userId = int.Parse(nameIdentifierClaim.Value);

            try
            {
                repositories.UserRepository.GetById(userId);
            }
            catch (DbNotFoundException)
            {
                throw new TokenValidationFailedException("User has been deactivated or deleted since last login time.");
            }
            
            return TaskCache.CompletedTask;
        }

        private void UpdateDatabaseToLatestVersion()
        {
            var migrator = new DbMigrator(_configuration.GetConnectionString("BlogsAppApi"));
            if (!migrator.IsDatabaseExist())
            {
                migrator.CreateDatabase();
                migrator.InitDatabase();
            }

            migrator.MigrateDatabaseToLatestVersion();
        }
    }
}

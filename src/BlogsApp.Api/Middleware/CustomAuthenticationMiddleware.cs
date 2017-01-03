using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using BlogsApp.Api.Exceptions;
using BlogsApp.Api.Models;
using BlogsApp.Common;
using BlogsApp.DataAccess.Exceptions;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BlogsApp.Api.Middleware
{
    /// <summary>
    /// Custom authentication middleware
    /// </summary>
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">Next middleware in pipeline</param>
        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="context">Http context</param>
        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var repositories = context.RequestServices.GetService<IRepositories>();
                var subClaim = context.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                var userId = int.Parse(subClaim.Value);

                try
                {
                    repositories.UserRepository.GetById(userId);
                }
                catch (DbNotFoundException)
                {
                    throw;
                }
            }

            await next(context);
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
                return;

            var code = HttpStatusCode.InternalServerError;

            if (exception is BlogsAppFriendlyException)
            {
                if (exception is DbNotFoundException) code = HttpStatusCode.NotFound;

                if (exception is LoginFailedException) code = HttpStatusCode.BadRequest;
                if (exception is AlreadyLoggedInException) code = HttpStatusCode.Forbidden;
            }
            

            await WriteExceptionAsync(context, exception, code);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(
                new HandledException
                {
                    Message = exception.Message,
                    Exception = exception.GetType().Name
                }));
        }
    }
}

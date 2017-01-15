using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using BlogsApp.DataAccess.Entities;
using BlogsApp.DataAccess.Exceptions;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BlogsApp.Api.Filters
{
    /// <summary>
    /// Custom authorization filter
    /// </summary>
    public class CustomAuthorizationFilter : Attribute, IAuthorizationFilter, IAsyncAuthorizationFilter
    {
        private readonly IRepositories _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories instance</param>
        public CustomAuthorizationFilter(IRepositories repositories)
        {
            _repositories = repositories;
        }

        /// <summary>
        /// On authorization handler
        /// </summary>
        /// <param name="context">Authorization filter context</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var claims = new List<Claim>
            //{
            //    new Claim("userId", "343"),
            //    new Claim("userRoleId", "1")
            //};
            //var identity = new ClaimsIdentity(claims);
            //var roles = new[] {"Admin"};
            
            //context.HttpContext.User = new GenericPrincipal(identity, roles);
        }

        /// <summary>
        /// On authorization async handler
        /// </summary>
        /// <param name="context">Authorization filter context</param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var attributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes();
                if (attributes.Any(x => x is AllowAnonymousAttribute))
                {
                    return;
                }
            }

            var subClaim = context.HttpContext.User.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            int userId;
            if (!int.TryParse(subClaim.Value, out userId))
            {
                throw new ArgumentException();
            }

            _repositories.UserRepository.GetById(userId);

            //context.HttpContext.User =
            //    new ClaimsPrincipal(
            //        new ClaimsIdentity(
            //            new List<Claim>
            //            {
            //                new Claim(ClaimTypes.NameIdentifier, userDb.UserId.ToString(), ClaimValueTypes.Integer32),
            //                new Claim(ClaimTypes.Role, roles[0], ClaimValueTypes.String)
            //            }, authScheme, ClaimTypes.NameIdentifier, ClaimTypes.Role));

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}

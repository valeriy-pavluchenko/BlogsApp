using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace BlogsApp.Api.Extensions
{
    /// <summary>
    /// Identity extensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Get user id
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>User id</returns>
        public static int GetUserId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            return claim == null ? 0 : int.Parse(claim.Value);
        }
    }
}

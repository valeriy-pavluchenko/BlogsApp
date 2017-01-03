using ApiModels = BlogsApp.Api.Models;
using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Converters
{
    /// <summary>
    /// User converter
    /// </summary>
    public static class UserRoleConverter
    {
        /// <summary>
        /// Converts db entity to api model
        /// </summary>
        /// <param name="userRole">User role db model</param>
        /// <returns>Api model</returns>
        public static ApiModels.UserRole ToApiModel(this DbEntities.UserRole userRole)
        {
            return new ApiModels.UserRole
            {
                UserRoleId = userRole.UserRoleId,
                Name = userRole.Name
            };
        }
    }
}

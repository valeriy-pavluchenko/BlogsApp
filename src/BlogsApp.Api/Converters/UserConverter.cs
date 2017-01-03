using ApiModels = BlogsApp.Api.Models.User;
using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Converters
{
    /// <summary>
    /// User converter
    /// </summary>
    public static class UserConverter
    {
        /// <summary>
        /// Converts db entity to api model
        /// </summary>
        /// <param name="user">User db model</param>
        /// <returns>Api model</returns>
        public static ApiModels.User ToApiModel(this DbEntities.User user)
        {
            return new ApiModels.User
            {
                UserId = user.UserId,
                Email = user.Email,
                RegisteredOn = user.RegisteredOnUtc
            };
        }
    }
}

using ApiModels = BlogsApp.Api.Models.Post;
using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Converters
{
    /// <summary>
    /// Post converter
    /// </summary>
    public static class PostConverter
    {
        /// <summary>
        /// Converts db entity to api model
        /// </summary>
        /// <param name="post">Post db model</param>
        /// <returns>Api model</returns>
        public static ApiModels.Post ToApiModel(this DbEntities.Post post)
        {
            return new ApiModels.Post
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Content = post.Content,
                AddedOn = post.AddedOnUtc
            };
        }
    }
}

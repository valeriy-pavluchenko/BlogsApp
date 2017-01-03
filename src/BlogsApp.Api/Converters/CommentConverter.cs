using ApiModels = BlogsApp.Api.Models.Comment;
using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Converters
{
    /// <summary>
    /// Comment converter
    /// </summary>
    public static class CommentConverter
    {
        /// <summary>
        /// Converts db entity to api model
        /// </summary>
        /// <param name="comment">Comment db model</param>
        /// <returns>Api model</returns>
        public static ApiModels.Comment ToApiModel(this DbEntities.Comment comment)
        {
            return new ApiModels.Comment
            {
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Message = comment.Message,
                AddedOn = comment.AddedOnUtc
            };
        }
    }
}

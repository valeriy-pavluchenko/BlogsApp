using System;

using BlogsApp.Api.Converters;
using BlogsApp.Api.Models.Comment;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;

using DbEnities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// Comment Controller
    /// </summary>
    [Route("api/comments")]
    public class CommentController : BaseController
    {
        private readonly IRepositories _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories instance</param>
        public CommentController(IRepositories repositories)
        {
            _repositories = repositories;
        }

        /// <summary>
        /// Get comment by id
        /// </summary>
        /// <param name="commentId">Comment id</param>
        /// <returns>Comments list</returns>
        [Route("{commentId}")]
        [HttpGet]
        [Produces(typeof(Comment))]
        public IActionResult GetCommentById(int commentId)
        {
            var commentDb = _repositories.CommentRepository.GetById(commentId);

            return Ok(commentDb.ToApiModel());
        }

        /// <summary>
        /// Add new comment
        /// </summary>
        /// <param name="comment">Comment data</param>
        [Route("")]
        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult AddComment([FromBody]NewComment comment)
        {
            var userDb = _repositories.UserRepository.GetById(comment.UserId);
            var postDb = _repositories.PostRepository.GetById(comment.PostId);

            var commentDb = new DbEnities.Comment
            {
                PostId = postDb.PostId,
                UserId = userDb.UserId,
                Message = comment.Message,
                AddedOnUtc = DateTime.Now.ToUniversalTime()
            };

            var commentId = _repositories.CommentRepository.Insert(commentDb);

            return Ok(commentId);
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        /// <param name="comment">Comment data</param>
        [Route("{commentId}")]
        [HttpPut]
        [Produces(typeof(void))]
        public IActionResult UpdateComment(int commentId, [FromBody]UpdateComment comment)
        {
            var commentDb = _repositories.CommentRepository.GetById(commentId);
            commentDb.Message = comment.Message;

            _repositories.CommentRepository.Update(commentDb);

            return Ok();
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="commentId">Comment id</param>
        [Route("{commentId}")]
        [HttpDelete]
        [Produces(typeof(void))]
        public IActionResult DeleteComment(int commentId)
        {
            var commentDb = _repositories.CommentRepository.GetById(commentId);
            _repositories.CommentRepository.Delete(commentDb.CommentId);

            return Ok();
        }
    }
}

using System;
using System.Linq;

using BlogsApp.Api.Converters;
using BlogsApp.Api.Models;
using BlogsApp.Api.Models.Comment;
using BlogsApp.Api.Models.Post;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// Post controller
    /// </summary>
    [Route("api/posts")]
    public class PostController : BaseController
    {
        private readonly IRepositories _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories instance</param>
        public PostController(IRepositories repositories)
        {
            _repositories = repositories;
        }

        /// <summary>
        /// Get posts list
        /// </summary>
        /// <param name="parameters">List parameters</param>
        /// <returns>Comments list</returns>
        [Route("")]
        [HttpGet]
        [Produces(typeof(Post[]))]
        public IActionResult GetPostsList([FromQuery]ListParameters parameters = null)
        {
            if (parameters == null)
                parameters = new ListParameters();

            var postsDb = _repositories.PostRepository.GetList(parameters.Limit, parameters.Offset);

            return Ok(postsDb.Select(x => x.ToApiModel()).ToArray());
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="postId">Post id</param>
        /// <returns>Post</returns>
        [Route("{postId}")]
        [HttpGet]
        [Produces(typeof(Post))]
        public IActionResult GetPostById(int postId)
        {
            var postDb = _repositories.PostRepository.GetById(postId);

            return Ok(postDb.ToApiModel());
        }

        /// <summary>
        /// Add post
        /// </summary>
        /// <param name="post">Post data</param>
        [Route("")]
        [HttpPost]
        [Produces(typeof(int))]
        public IActionResult AddPost([FromBody]NewPost post)
        {
            var userDb = _repositories.UserRepository.GetById(post.UserId);
            var postDb = new DbEntities.Post
            {
                UserId = userDb.UserId,
                Title = post.Title,
                Content = post.Content,
                AddedOnUtc = DateTime.Now.ToUniversalTime()
            };

            var postId = _repositories.PostRepository.Insert(postDb);

            return Ok(postId);
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="postId">Post id</param>
        /// <param name="post">Post data</param>
        [Route("{postId}")]
        [HttpPut]
        [Produces(typeof(void))]
        public IActionResult UpdatePost(int postId, [FromBody]UpdatePost post)
        {
            var postDb = _repositories.PostRepository.GetById(postId);
            postDb.Title = post.Title;
            postDb.Content = post.Content;

            _repositories.PostRepository.Update(postDb);

            return Ok();
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="postId">Post id</param>
        [Route("{postId}")]
        [HttpDelete]
        [Produces(typeof(void))]
        public IActionResult DeletePost(int postId)
        {
            var postDb = _repositories.PostRepository.GetById(postId);
            _repositories.PostRepository.Delete(postDb.PostId);

            return Ok();
        }

        /// <summary>
        /// Get comments list by post id
        /// </summary>
        /// <param name="postId">Post id</param>
        /// <param name="parameters">List parameters</param>
        /// <returns>Comments list</returns>
        [Route("{postId}/comments")]
        [HttpGet]
        [Produces(typeof(Comment[]))]
        public IActionResult GetCommentsByPostId(int postId, [FromQuery]ListParameters parameters = null)
        {
            if (parameters == null)
                parameters = new ListParameters();

            var postDb = _repositories.PostRepository.GetById(postId);
            var commentsDb = _repositories.CommentRepository.GetListByPostId(postDb.PostId, parameters.Limit, parameters.Offset);

            return Ok(commentsDb.Select(x => x.ToApiModel()).ToArray());
        }
    }
}

using BlogsApp.Api.Converters;
using BlogsApp.Api.Models;
using BlogsApp.Api.Models.Comment;
using BlogsApp.Api.Models.Post;
using BlogsApp.Api.Models.User;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;

using BlogsApp.Api.Extensions;
using BlogsApp.DataAccess.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

using Db = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IRepositories _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories instance</param>
        public UserController(IRepositories repositories)
        {
            _repositories = repositories;
        }

        /// <summary>
        /// Get users list
        /// </summary>
        /// <param name="parameters">List parameters</param>
        /// <returns>Users list</returns>
        [Route("")]
        [HttpGet]
        [Produces(typeof(User[]))]
        public IActionResult GetUsersList([FromQuery]ListParameters parameters = null)
        {
            if (parameters == null)
                parameters = new ListParameters();

            var usersDb = _repositories.UserRepository.GetList(parameters.Limit, parameters.Offset);

            return Ok(usersDb.Select(x => x.ToApiModel()).ToArray());
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        [Route("{userId}")]
        [HttpGet]
        [Produces(typeof(User))]
        [Authorize(Roles = "User")]
        public IActionResult GetUserById(int userId)
        {
            var user = _repositories.UserRepository.GetById(userId);

            return Ok(user.ToApiModel());
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId">User id</param>
        [Route("{userId}")]
        [HttpDelete]
        [Produces(typeof(void))]
        public IActionResult DeleteUser(int userId)
        {
            var userDb = _repositories.UserRepository.GetById(userId);
            _repositories.UserRepository.Delete(userDb.UserId);

            return Ok();
        }

        /// <summary>
        /// Get comments list by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="parameters">List parameters</param>
        /// <returns>Comments list</returns>
        [Route("{userId}/comments")]
        [HttpGet]
        [Produces(typeof(Comment[]))]
        public IActionResult GetCommentsByUserId(int userId, [FromQuery]ListParameters parameters = null)
        {
            if (parameters == null)
                parameters = new ListParameters();

            var userDb = _repositories.UserRepository.GetById(userId);
            var commentsDb = _repositories.CommentRepository.GetListByUserId(userDb.UserId, parameters.Limit, parameters.Offset);

            return Ok(commentsDb.Select(x => x.ToApiModel()).ToArray());
        }

        /// <summary>
        /// Get posts list by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="parameters">List parameters</param>
        /// <returns>Posts list</returns>
        [Route("{userId}/posts")]
        [HttpGet]
        [Produces(typeof(Post[]))]
        public IActionResult GetPostsByUserId(int userId, [FromQuery]ListParameters parameters = null)
        {
            if (parameters == null)
                parameters = new ListParameters();

            var userDb = _repositories.UserRepository.GetById(userId);
            var postsDb = _repositories.PostRepository.GetListByUserId(userDb.UserId, parameters.Limit, parameters.Offset);

            return Ok(postsDb.Select(x => x.ToApiModel()).ToArray());
        }

        /// <summary>
        /// Get user's roles list by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User's roles list</returns>
        [Route("{userId}/user_roles")]
        [HttpGet]
        [Produces(typeof(UserRole[]))]
        public IActionResult GetUserRolesByUserId(int userId)
        {
            var userDb = _repositories.UserRepository.GetById(userId);
            var userRolesDb = _repositories.UserRoleRepository.GetListByUserId(userDb.UserId);

            return Ok(userRolesDb.Select(x => x.ToApiModel()).ToArray());
        }

        /// <summary>
        /// Promotes user with new role
        /// </summary>
        /// <param name="userId">User id</param>
        /// /// <param name="userRoleId">User's role id</param>
        /// <returns>User's roles list</returns>
        [Route("{userId}/user_roles/{userRoleId}")]
        [HttpPost]
        [Produces(typeof(void))]
        public IActionResult PromoteUserWithUserRole(int userId, int userRoleId)
        {
            var user = _repositories.UserRepository.GetById(userId);
            var userRole = _repositories.UserRoleRepository.GetById(userRoleId);

            var userRoles = _repositories.UserRoleRepository.GetListByUserId(user.UserId);
            if (userRoles.Any(x => x.UserRoleId == userRole.UserRoleId))
            {
                return BadRequest("User already in this role");
            }

            _repositories.UserRoleUserRepository.Insert(user.UserId, userRole.UserRoleId);

            return Ok();
        }

        /// <summary>
        /// Deletes user's role from user
        /// </summary>
        /// <param name="userId">User id</param>
        /// /// <param name="userRoleId">User's role id</param>
        /// <returns>User's roles list</returns>
        [Route("{userId}/user_roles/{userRoleId}")]
        [HttpDelete]
        [Produces(typeof(void))]
        public IActionResult DeleteUserRoleFromUser(int userId, int userRoleId)
        {
            var user = _repositories.UserRepository.GetById(userId);
            var userRole = _repositories.UserRoleRepository.GetById(userRoleId);

            var userRoles = _repositories.UserRoleRepository.GetListByUserId(user.UserId);
            if (userRoles.All(x => x.UserRoleId != userRole.UserRoleId))
            {
                return BadRequest("User is not in this role");
            }

            _repositories.UserRoleUserRepository.Delete(user.UserId, userRole.UserRoleId);

            return Ok();
        }
    }
}

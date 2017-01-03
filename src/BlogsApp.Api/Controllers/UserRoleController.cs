using BlogsApp.Api.Converters;
using BlogsApp.Api.Models;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// User's role controller
    /// </summary>
    [Route("api/user_roles")]
    public class UserRoleController : BaseController
    {
        private readonly IRepositories _repositories;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories instance</param>
        public UserRoleController(IRepositories repositories)
        {
            _repositories = repositories;
        }

        /// <summary>
        /// Get user's role by id
        /// </summary>
        /// <param name="userRoleId">User's role id</param>
        /// <returns>User's role</returns>
        [Route("{userRoleId}")]
        [HttpGet]
        [Produces(typeof(UserRole))]
        public IActionResult GetUserRoleById(int userRoleId)
        {
            var userRoleDb = _repositories.UserRoleRepository.GetById(userRoleId);
            return Ok(userRoleDb.ToApiModel());
        }

        /// <summary>
        /// Get user's roles list
        /// </summary>
        /// <returns>User's roles list</returns>
        [Route("")]
        [HttpGet]
        [Produces(typeof(UserRole[]))]
        public IActionResult GetUserRolesList()
        {
            var userRolesDb = _repositories.UserRoleRepository.GetList();
            return Ok(userRolesDb.Select(x => x.ToApiModel()).ToArray());
        }
    }
}

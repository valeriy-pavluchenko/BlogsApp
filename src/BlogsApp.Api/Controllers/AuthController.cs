using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;

using BlogsApp.Api.Exceptions;
using BlogsApp.Api.Extensions;
using BlogsApp.Api.Models.Auth;
using BlogsApp.DataAccess.Enums;
using BlogsApp.DataAccess.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

using Crypter = BCrypt.Net.BCrypt;
using DbEntities = BlogsApp.DataAccess.Entities;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IRepositories _repositories;
        private readonly IConfigurationRoot _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repositories">Repositories</param>
        /// /// <param name="configuration">Configuration</param>
        public AuthController(IRepositories repositories, IConfigurationRoot configuration)
        {
            _repositories = repositories;
            _configuration = configuration;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registrationRequest">Registration request data</param>
        [Route("register")]
        [HttpPost]
        [Produces(typeof(void))]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegistrationRequest registrationRequest)
        {
            var newUser = new DbEntities.User
            {
                Email = registrationRequest.Email.Trim().ToLower(),
                PasswordHash = Crypter.HashPassword(registrationRequest.Password),
                RegisteredOnUtc = DateTime.Now.ToUniversalTime()
            };

            var userId = _repositories.UserRepository.Insert(newUser);
            _repositories.UserRoleUserRepository.Insert(userId, (int)UserRoleEnum.User);

            return Ok();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="loginRequest">Login request data</param>
        [Route("login")]
        [HttpPost]
        [Produces(typeof(string))]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginRequest loginRequest)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // if user exist, return token in headers
                var userDb = _repositories.UserRepository.GetByEmail(loginRequest.Email);
                if (userDb == null || !Crypter.Verify(loginRequest.Password, userDb.PasswordHash))
                {
                    throw new LoginFailedException("Incorrect email or password");
                }

                var token = GenerateToken(userDb.UserId);
                return Ok(token);
            }

            throw new AlreadyLoggedInException("User already logged in");
        }

        /// <summary>
        /// Create a Jwt with user information
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Token</returns>
        private string GenerateToken(int userId)
        {
            var user = _repositories.UserRepository.GetById(userId);
            var userRoles = _repositories.UserRoleRepository.GetListByUserId(user.UserId);

            var now = DateTime.UtcNow;
            var expiresOn = now.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString(), ClaimValueTypes.Integer32),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };
            claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole.Name, ClaimValueTypes.String)));

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetAppSettings("SecretKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration.GetAppSettings("Issuer"),
                audience: _configuration.GetAppSettings("Audience"),
                claims: claims,
                notBefore: now,
                expires: expiresOn,
                signingCredentials: credentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtSecurityTokenHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// Logout user
        /// </summary>
        [Route("logout")]
        [HttpPost]
        [Produces(typeof(void))]
        public IActionResult Logout()
        {
            // if user logged in, clear token in headers or ser expiration date to now
            return Ok();
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using BlogsApp.Api.Attributes;
using BlogsApp.Api.Extensions;
using BlogsApp.Api.Filters;
using BlogsApp.Api.Middleware;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [ValidateModel]
    [Authorize]
    public class BaseController : Controller
    {
    }
}

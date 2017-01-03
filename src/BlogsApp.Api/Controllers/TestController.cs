using Microsoft.AspNetCore.Mvc;

using System.Reflection;

namespace BlogsApp.Api.Controllers
{
    /// <summary>
    /// Test Controller
    /// </summary>
    [Route("api/test")]
    public class TestController : Controller
    {
        /// <summary>
        /// Echo text
        /// </summary>
        /// <param name="text">Text for echo</param>
        /// <returns>Returns an echo text</returns>
        [Route("echo")]
        [HttpGet]
        [Produces(typeof(string))]
        public IActionResult Echo([FromQuery]string text)
        {
            return Ok(text);
        }

        /// <summary>
        /// Get API version
        /// </summary>
        /// <returns>Returns API version</returns>
        [Route("version")]
        [HttpGet]
        [Produces(typeof(string))]
        public IActionResult Version()
        {
            var version = this.GetType().GetTypeInfo().Assembly.GetName().Version.ToString();
            return Ok(version);
        }
    }
}

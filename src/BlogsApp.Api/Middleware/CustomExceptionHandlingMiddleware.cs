using System;
using System.Net;
using System.Threading.Tasks;

using BlogsApp.Api.Exceptions;
using BlogsApp.Api.Models;
using BlogsApp.Common;
using BlogsApp.DataAccess.Exceptions;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogsApp.Api.Middleware
{
    /// <summary>
    /// Exception hadnling middleware
    /// </summary>
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">Next middleware in pipeline</param>
        public CustomExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="context">Http context</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
                return;

            var code = HttpStatusCode.InternalServerError;

            if (exception is BlogsAppFriendlyException)
            {
                // DB exceptions
                if (exception is DbNotFoundException) code = HttpStatusCode.NotFound;

                // Auth exceptions
                if (exception is LoginFailedException) code = HttpStatusCode.BadRequest;
                if (exception is AlreadyLoggedInException) code = HttpStatusCode.Forbidden;
                if (exception is TokenValidationFailedException) code = HttpStatusCode.Unauthorized;
            }

            await WriteExceptionAsync(context, exception, code);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            await response.WriteAsync(JsonConvert.SerializeObject(
                new HandledException
                {
                    Message = exception.Message,
                    Exception = exception.GetType().Name
                }));
        }
    }
}

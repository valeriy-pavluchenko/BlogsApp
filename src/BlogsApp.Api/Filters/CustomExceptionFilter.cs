using BlogsApp.Api.Models;
using BlogsApp.Common;
using BlogsApp.DataAccess.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

using System;
using System.Net;
using System.Threading.Tasks;

namespace BlogsApp.Api.Filters
{
    /// <summary>
    /// Custom exception filter
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        /// <summary>
        /// Hanldes exceptions when exception throws
        /// </summary>
        /// <param name="context">Exception context</param>
        public void OnException(ExceptionContext context)
        {
            HandleException(context).ConfigureAwait(false);
        }

        /// <summary>
        /// Hanldes exceptions when exception throws in async manner
        /// </summary>
        /// <param name="context">Exception context</param>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await HandleException(context);
        }

        private async Task HandleException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;

            if (context.Exception is BlogsAppFriendlyException)
            {
                if (context.Exception is DbNotFoundException)
                {
                    status = HttpStatusCode.NotFound;
                }

                context.ExceptionHandled = true;
            }

            if (context.Exception is NotImplementedException)
            {
                status = HttpStatusCode.NotImplemented;
            }

            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;

            await WriteJsonObjectResult(context);
        }

        private async Task WriteJsonObjectResult(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject(
                new HandledException
                {
                    Message = context.Exception.Message,
                    Exception = context.Exception.GetType().Name
                }));
        }

        private async Task WriteTextPlainResult(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/plain";
            await response.WriteAsync(context.Exception.Message);
        }
    }
}

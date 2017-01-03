using BlogsApp.Common;

namespace BlogsApp.Api.Exceptions
{
    /// <summary>
    /// Exception occurs when the post validation challenge has been failed
    /// </summary>
    public class TokenValidationFailedException : BlogsAppFriendlyException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public TokenValidationFailedException(string message) : base(message)
        {
        }
    }
}

using BlogsApp.Common;

namespace BlogsApp.Api.Exceptions
{
    /// <summary>
    /// Exception occurs when the login attempt was failed
    /// </summary>
    public class LoginFailedException : BlogsAppFriendlyException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public LoginFailedException(string message) : base(message)
        {
        }
    }
}

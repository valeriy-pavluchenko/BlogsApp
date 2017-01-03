using BlogsApp.Common;

namespace BlogsApp.Api.Exceptions
{
    /// <summary>
    /// Exception occurs when the user try to login again
    /// </summary>
    public class AlreadyLoggedInException : BlogsAppFriendlyException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public AlreadyLoggedInException(string message) : base(message)
        {
        }
    }
}

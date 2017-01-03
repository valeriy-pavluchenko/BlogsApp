using System;

namespace BlogsApp.Common
{
    /// <summary>
    /// Application inner exception that expected to be handled
    /// </summary>
    public class BlogsAppFriendlyException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public BlogsAppFriendlyException(string message) : base(message)
        {
        }
    }
}

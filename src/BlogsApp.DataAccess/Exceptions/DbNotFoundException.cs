using BlogsApp.Common;

namespace BlogsApp.DataAccess.Exceptions
{
    /// <summary>
    /// Exception occurs when the record has't been founded in the database
    /// </summary>
    public class DbNotFoundException : BlogsAppFriendlyException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public DbNotFoundException(string message) : base(message)
        {
        }
    }
}

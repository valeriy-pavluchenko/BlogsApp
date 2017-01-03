using BlogsApp.Common;

namespace BlogsApp.DataAccess.Exceptions
{
    /// <summary>
    /// Exception occurs when such record is already exist in the database
    /// </summary>
    public class DbRecordAlreadyExistException : BlogsAppFriendlyException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public DbRecordAlreadyExistException(string message) : base(message)
        {
        }
    }
}

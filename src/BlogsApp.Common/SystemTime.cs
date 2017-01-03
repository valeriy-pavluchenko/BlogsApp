using System;

namespace BlogsApp.Common
{
    /// <summary>
    /// Time provider
    /// </summary>
    public class SystemTime
    {
        /// <summary>
        /// Current time
        /// </summary>
        public Func<DateTime> Now = () => DateTime.Now;

        /// <summary>
        ///  Set time to return when SystemTime.Now() is called.
        /// </summary>
        public void SetDateTime(DateTime dateTimeNow)
        {
            Now = () => dateTimeNow;
        }
    }
}

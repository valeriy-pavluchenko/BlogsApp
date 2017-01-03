using System;

namespace BlogsApp.Api.Extensions
{
    /// <summary>
    /// DateTime extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// To unix time seconds
        /// </summary>
        /// <param name="dateTime">DateTime</param>
        /// <returns>Unix time seconds</returns>
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            return (long) dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}

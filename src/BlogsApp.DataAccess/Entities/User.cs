using System;

namespace BlogsApp.DataAccess.Entities
{
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisteredOnUtc { get; set; }
    }
}

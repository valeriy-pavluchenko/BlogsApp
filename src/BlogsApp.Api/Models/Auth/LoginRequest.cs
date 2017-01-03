using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Auth
{
    /// <summary>
    /// Login request
    /// </summary>
    [DataContract(Name = "loginRequest")]
    public class LoginRequest
    {
        /// <summary>
        /// User's email
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// User's passwors
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}

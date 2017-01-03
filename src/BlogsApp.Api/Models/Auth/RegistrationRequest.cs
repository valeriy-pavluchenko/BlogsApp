using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Auth
{
    /// <summary>
    /// Registration request
    /// </summary>
    [DataContract(Name = "registrationRequest")]
    public class RegistrationRequest
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

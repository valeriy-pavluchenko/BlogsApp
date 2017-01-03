using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.User
{
    /// <summary>
    /// User
    /// </summary>
    [DataContract(Name = "user")]
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [DataMember(Name = "registeredOn")]
        public DateTime RegisteredOn { get; set; }
    }
}

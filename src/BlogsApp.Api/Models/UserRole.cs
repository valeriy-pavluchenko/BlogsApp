using System.Runtime.Serialization;

namespace BlogsApp.Api.Models
{
    /// <summary>
    /// User's role
    /// </summary>
    [DataContract(Name = "userRole")]
    public class UserRole
    {
        /// <summary>
        /// User's role id
        /// </summary>
        [DataMember(Name = "userRoleId")]
        public int UserRoleId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

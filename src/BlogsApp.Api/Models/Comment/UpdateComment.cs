using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Comment
{
    /// <summary>
    /// Comment
    /// </summary>
    [DataContract(Name = "comment")]
    public class UpdateComment
    {
        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}

using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Comment
{
    /// <summary>
    /// Comment
    /// </summary>
    [DataContract(Name = "comment")]
    public class NewComment : UpdateComment
    {
        /// <summary>
        /// Post Id
        /// </summary>
        [DataMember(Name = "postId")]
        public int PostId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }
    }
}

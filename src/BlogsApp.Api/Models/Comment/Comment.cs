using System;
using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Comment
{
    /// <summary>
    /// Comment
    /// </summary>
    [DataContract(Name = "comment")]
    public class Comment : NewComment
    {
        /// <summary>
        /// Comment Id
        /// </summary>
        [DataMember(Name = "commentId")]
        public int CommentId { get; set; }

        /// <summary>
        /// Added On
        /// </summary>
        [DataMember(Name = "addedOn")]
        public DateTime AddedOn { get; set; }
    }
}

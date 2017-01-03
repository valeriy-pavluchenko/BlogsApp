using System;
using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Post
{
    /// <summary>
    /// Post
    /// </summary>
    [DataContract(Name = "post")]
    public class Post : NewPost
    {
        /// <summary>
        /// Post Id
        /// </summary>
        [DataMember(Name = "postId")]
        public int PostId { get; set; }

        /// <summary>
        /// Added On
        /// </summary>
        [DataMember(Name = "addedOn")]
        public DateTime AddedOn { get; set; }
    }
}

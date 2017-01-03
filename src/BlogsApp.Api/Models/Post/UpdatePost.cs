using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Post
{
    /// <summary>
    /// Post
    /// </summary>
    [DataContract(Name = "post")]
    public class UpdatePost
    {
        /// <summary>
        /// Title
        /// </summary>
        [Required]
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }
}

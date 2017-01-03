using System.Runtime.Serialization;

namespace BlogsApp.Api.Models.Post
{
    /// <summary>
    /// Post
    /// </summary>
    [DataContract(Name = "post")]
    public class NewPost : UpdatePost
    {
        /// <summary>
        /// User Id
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }
    }
}

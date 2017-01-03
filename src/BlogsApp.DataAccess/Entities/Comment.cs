using System;

namespace BlogsApp.DataAccess.Entities
{
    /// <summary>
    /// Comment
    /// </summary>
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime AddedOnUtc { get; set; }
    }
}

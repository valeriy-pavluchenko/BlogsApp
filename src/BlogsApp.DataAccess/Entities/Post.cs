﻿using System;

namespace BlogsApp.DataAccess.Entities
{
    /// <summary>
    /// Post
    /// </summary>
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddedOnUtc { get; set; }
    }
}

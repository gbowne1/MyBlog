using System;

namespace MyBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public ApplicationUser Author { get; set; }
        public DateTime DatePosted { get; set; }
    }
}

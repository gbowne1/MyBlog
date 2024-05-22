using System.Collections.Generic;

namespace MyBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }

        public ICollection<TagBlogPosts> TagBlogPosts { get; set; } // Navigation property
    }
}
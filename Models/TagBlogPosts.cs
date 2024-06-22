using System.Collections.Generic;

namespace MyBlog.Models
{
    public class TagBlogPosts
    {
        public int TagId { get; set; }
        public Tag? Tag { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<BlogPost>? BlogPosts { get; set; }
        public ICollection<BlogPost>? Posts { get; set; }
    }
}

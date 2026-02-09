using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship to BlogPost
        public int BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }

        // Relationship to IdentityUser
        public string? AuthorId { get; set; }
        public IdentityUser? Author { get; set; }
    }
}

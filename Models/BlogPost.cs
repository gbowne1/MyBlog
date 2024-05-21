using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public DateTime PublishDate { get; set; }

        public string ImageUrl { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}

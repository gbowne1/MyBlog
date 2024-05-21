using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Models;

namespace MyBlog.Models
{

    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        // Other DbSet properties for your entities

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                );
        }

        public class TagBlogPosts
        {
            public int TagId { get; set; }
            public Tag Tag { get; set; }
            public int BlogPostId { get; set; }
            public BlogPost BlogPost { get; set; }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(255);

            // Configure BlogPost-Category relationship
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId);

            // Configure User-Post relationship
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Author)  // One-to-many with ApplicationUser
                .WithMany(u => u.Posts)  // Author can have many posts
                .HasForeignKey(p => p.ApplicationUserId);    // Blog post can have many tags

            // Configure Comment-BlogPost relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.BlogPostId);

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.BlogPosts) // Tag can have many BlogPosts
                .WithMany(p => p.Tags) // BlogPost can have many Tags (configured later)
                .UsingEntity<TagBlogPosts>(
                    "TagBlogPost",  // Table name for the junction table
                    j => j.HasOne(t => t.Tag).WithMany(t => t.TagBlogPosts).HasForeignKey(t => t.TagId),
                    j => j.HasOne(p => p.BlogPost).WithMany(p => p.TagBlogPosts).HasForeignKey(p => p.BlogPostId)
                );

            // Then, configure the relationship from BlogPost's perspective
            modelBuilder.Entity<BlogPost>()
                .WithMany(p => p.Tags) // Redundant but explicit (BlogPost can have many Tags)
                .HasForeignKey(p => p.BlogPostId); // Foreign key for the many-to-many relationship (already defined in TagBlogPosts)

        }
    }
}

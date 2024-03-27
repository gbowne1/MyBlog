using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;

namespace MyBlog.Models
{

    public class ApplicationDbContext : DbContext
    {
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
            var connectionStriong = Environment.GetEnvironmentVariable("MyBlogDbConnectionString");
            optionsBuilder.UseSqlServer(connectionString,
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null)
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(255);

            // Configure BlogPost-Category relationship once (remove the duplicate):
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId);

            // Choose either one of these for the user-post relationship:
            modelBuilder.Entity<BlogPost>()
                .HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.ApplicationUserId);

            // Configure Comment-BlogPost relationship
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BlogPost)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.BlogPostId);

             modelBuilder.Entity<Tag>()
                .HasMany(t => t.BlogPosts)
                .WithMany(p => p.Tags) // Assuming a many-to-many relationship
                .UsingEntity<Dictionary<string, object>>(
                    "TagBlogPost",
                    j => j
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId"),
                    j => j
                        .HasOne<BlogPost>()
                        .WithMany()
                        .HasForeignKey("BlogPostId"));
                }
    }
}

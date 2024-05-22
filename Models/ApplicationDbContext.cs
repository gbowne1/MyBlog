using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBlog.Models;

namespace MyBlog.Models
{
    public class ApplicationDbContext : DbContext
    {
        public IConfiguration _configuration;

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
            .HasForeignKey(p => p.ApplicationUserId);

            // Configure Comment-BlogPost relationship
            modelBuilder.Entity<Comment>()
            .HasOne(c => c.BlogPost)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.BlogPostId);

            modelBuilder.Entity<Tag>()
            .HasMany(t => t.TagBlogPosts)
            .WithOne(tb => tb.Tag)
            .HasForeignKey(tb => tb.TagId);

            // Correctly configure the many-to-many relationship between Tag and BlogPost via TagBlogPosts
            modelBuilder.Entity<BlogPost>()
            .HasMany(bp => bp.TagBlogPosts)
            .WithOne(tb => tb.BlogPost)
            .HasForeignKey(tb => tb.BlogPostId);

            modelBuilder.Entity<TagBlogPosts>()
            .HasKey(tb => new { tb.TagId, tb.BlogPostId });

        }
    }
}
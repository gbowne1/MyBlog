using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            BlogPosts = Set<BlogPost>();
        }
    }
}

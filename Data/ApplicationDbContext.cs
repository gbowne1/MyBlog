using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;
using Microsoft.AspNetCore.Identity; // Added for IdentityUser

namespace MyBlog.Data
{
    // CORRECTED: Inherit from IdentityDbContext<IdentityUser>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // BlogPosts table setup is correct
        public DbSet<BlogPost> BlogPosts { get; set; } = default!;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;

namespace MyBlog.Models {

	public class ApplicationUser : IdentityUser
	{

	}
	public class ApplicationDbContext : DbContext
	{
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

		// Other DbSet properties for your entities

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
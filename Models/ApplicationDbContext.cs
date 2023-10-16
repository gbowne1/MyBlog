using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models {

	public class ApplicationUser : IdentityUser
	{

	}
	public class ApplicationDbContext : DbContext
	{
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }

		// Other DbSet properties for your entities

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
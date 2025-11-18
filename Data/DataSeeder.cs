using MyBlog.Models;

namespace MyBlog.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.BlogPosts.Any())
            {
                context.BlogPosts.Add(new BlogPost
                {
                    Title = "Welcome to My Blog",
                    Content = "This is your first blog post!",
                    CreatedAt = DateTime.UtcNow
                });
                context.SaveChanges();
            }
        }
    }
}

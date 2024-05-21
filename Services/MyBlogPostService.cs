using System.Collections.Generic;
using MyBlog.Models;
using System.Linq;

namespace MyBlog.Services
{
    public class MyBlogPostService : IBlogPostService
    {
        // Add fields or properties if needed for service functionality
        private readonly ApplicationDbContext _context; // Assuming DbContext for data access

        public MyBlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            // Implement logic to retrieve all blog posts from the database using _context
            // This might involve querying the BlogPost table
            // return a collection of BlogPost objects
            return _context.BlogPosts.AsEnumerable().OrderByDescending(p => p.PublishDate).ToList();
        }

        public BlogPost GetBlogPostById(int id)
        {
            // Implement logic to retrieve a blog post by ID from the database
            // using your ApplicationDbContext instance
            // return the BlogPost object if found, otherwise return null
            return _context.BlogPosts.FirstOrDefault(p => p.Id == id);
        }

        public void CreateBlogPost(BlogPost blogPost)
        {
            // Implement logic to add the new blog post to the database
            // using _context.BlogPosts.Add(blogPost) and _context.SaveChanges()
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            // Implement logic to update an existing blog post in the database
            // You might need to find the blog post by ID first and then update its properties
            // using _context.BlogPosts.Update(blogPost) and _context.SaveChanges()
        }

        public void DeleteBlogPost(int id)
        {
            // Implement logic to delete a blog post by ID from the database
            // You might need to find the blog post by ID first and then remove it
            // using _context.BlogPosts.Remove(blogPost) and _context.SaveChanges()
        }
    }
}

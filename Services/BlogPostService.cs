using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

namespace MyBlog.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public BlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all blog posts
        public IEnumerable<BlogPost> GetBlogPosts()
        {
            return _context.BlogPosts.ToList();
        }

        // Retrieve a blog post by its ID
        public BlogPost? GetBlogPostById(int id)
        {
            return _context.BlogPosts.Find(id);
        }

        // Create a new blog post
        public void CreateBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            _context.SaveChanges();
        }

        // Update an existing blog post
        public void UpdateBlogPost(BlogPost blogPost)
        {
            _context.Entry(blogPost).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // Delete a blog post by ID
        public void DeleteBlogPost(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
                _context.SaveChanges();
            }
        }
    }
}

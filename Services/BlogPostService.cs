using System.Collections.Generic;
using MyBlog.Models; // Make sure this matches the namespace where your BlogPost model is located
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly ApplicationDbContext _context; // Assuming you have a DbContext named MyBlogContext

        public BlogPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BlogPost> GetBlogPosts()
        {
            return _context.BlogPosts.ToList();
        }

        public BlogPost GetBlogPostById(int id)
        {
            return _context.BlogPosts.Find(id);
        }

        public void CreateBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            _context.SaveChanges();
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            _context.Entry(blogPost).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBlogPost(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if (blogPost!= null)
            {
                _context.BlogPosts.Remove(blogPost);
                _context.SaveChanges();
            }
        }
    }
}
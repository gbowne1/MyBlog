using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;

            // Seed if empty
            if (!_context.BlogPosts.Any())
            {
                _context.BlogPosts.Add(new BlogPost
                {
                    Title = "Welcome to My Blog",
                    Content = "This is your first blog post!",
                    CreatedAt = DateTime.UtcNow
                });
                _context.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            var posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
            return View(posts);
        }
    }
}

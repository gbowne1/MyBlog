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
        }

        public IActionResult Index()
        {
            var posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
            return View(posts);
        }
    }
}


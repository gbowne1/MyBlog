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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.UtcNow;

                _context.BlogPosts.Add(post);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }
    }
}

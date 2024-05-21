using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BlogController> _logger;

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }

        public BlogController(ILogger<BlogController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Default action method to list blog posts
        public async Task<IActionResult> Index()
        {
            var model = await _context.BlogPosts.ToListAsync();
            return View(model);
        }

        // Action method to display a single blog post
        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index"); // Redirect to homepage if search term is empty
            }

            var model = await _context.BlogPosts
                .Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm))
                .ToListAsync();

            return View(model);
        }
        // Other action methods...
    }
}

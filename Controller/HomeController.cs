using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models;
using System.Diagnostics;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        // 1. Declare a private field for the database context
        private readonly ApplicationDbContext _context;

        // 2. Inject the context into the constructor
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 3. Update Index() to fetch recent posts
        public async Task<IActionResult> Index()
        {
            // Fetch the 3 most recent blog posts
            // We order by creation date descending and take only 3.
            var recentPosts = await _context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .Take(3) // Limits the results
                .ToListAsync();

            // Pass the list of recent posts to the View
            return View(recentPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Data;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // READ: Display all posts
        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(posts);
        }

        // READ: Display a single post
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // CREATE: GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.UtcNow;
                post.AuthorId = _userManager.GetUserId(User);

                _context.BBlogPosts.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // UPDATE: GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.BBlogPosts.FindAsync(id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // UPDATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost post)
        {
            if (id != post.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existingPost = await _context.BBlogPosts.FindAsync(id);

                if (existingPost == null)
                    return NotFound();

                // Update only editable fields
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;

                _context.Update(existingPost);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = post.Id });
            }

            return View(post);
        }

        // DELETE: GET
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.BBlogPosts.FindAsync(id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        // DELETE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.BBlogPosts.FindAsync(id);

            if (post != null)
            {
                _context.BBlogPosts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.BBlogPosts.Any(e => e.Id == id);
        }
    }
}

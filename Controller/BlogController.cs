using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // READ: Display all posts
        public async Task<IActionResult> Index()
        {
            // OPTIMIZATION: Changed ToList() to ToListAsync()
            var posts = await _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View(posts);
        }

        // READ: Display a single post (Details)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null) return NotFound();

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

                _context.BlogPosts.Add(post);
                // OPTIMIZATION: Changed SaveChanges() to SaveChangesAsync()
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(post);
        }

        // UPDATE: GET
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null) return NotFound();

            return View(post);
        }

        // UPDATE: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost post)
        {
            if (id != post.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // FIX: Prevent overwriting CreatedAt with default date
                    // 1. Get the existing post from DB (AsNoTracking is not needed here as we want to update it)
                    var existingPost = await _context.BlogPosts.FindAsync(id);
                    
                    if (existingPost == null) return NotFound();

                    // 2. Update only the editable fields
                    existingPost.Title = post.Title;
                    existingPost.Content = post.Content;
                    // Add other fields here if you have them (e.g. ImageUrl)
                    
                    // Note: We do NOT update existingPost.CreatedAt
                    
                    _context.Update(existingPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = post.Id });
            }
            return View(post);
        }

        // DELETE: GET
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null) return NotFound();

            return View(post);
        }

        // DELETE: POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);

            if (post != null)
            {
                _context.BlogPosts.Remove(post);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper method to check existence
        private bool PostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}

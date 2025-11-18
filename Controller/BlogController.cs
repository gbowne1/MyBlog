using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Required for FindAsync and other EF methods
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
        public IActionResult Index()
        {
            var posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
            return View(posts);
        }
        
        // READ: Display a single post (Details)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Find the post by ID
            var post = await _context.BlogPosts.FindAsync(id);
            
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // CREATE: GET action to display the creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE: POST action to handle form submission
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

        // UPDATE: GET action to display the edit form
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var post = await _context.BlogPosts.FindAsync(id);
            
            if (post == null)
            {
                return NotFound();
            }
            
            return View(post);
        }

        // UPDATE: POST action to handle save changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Mark the post as modified and save changes
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Optional: Add logic here to handle concurrent updates if needed
                    if (!_context.BlogPosts.Any(e => e.Id == id))
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

        // DELETE: GET action to show the confirmation screen
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.BlogPosts.FindAsync(id);
            
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // DELETE: POST action to execute the deletion
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
    }
}

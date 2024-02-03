using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    [Route("api/[controller]")]
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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _context.BlogPosts.ToListAsync();
            return View(model);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error message to the user
                    _logger.LogError(ex, "An error occurred while updating the blog post.");
                    return StatusCode(500, "Internal server error");
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public IActionResult Details(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }
    }
}

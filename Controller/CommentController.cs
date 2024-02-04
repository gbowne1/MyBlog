using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Comment/
        public async Task<IActionResult> Index()
        {
            var comments = await _context.Comments.Include(c => c.Author).Include(c => c.BlogPost).ToListAsync();
            return View(comments);
        }

        // GET: /Comment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // Other action methods for editing, deleting, etc.
    }
}

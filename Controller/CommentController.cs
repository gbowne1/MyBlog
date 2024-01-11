using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class CommentController : Controller
    {
        // GET: /Comment/
        public IActionResult Index()
        {
            // Your logic to retrieve and display comments
            return View();
        }

        // GET: /Comment/Create
        public IActionResult Create()
        {
            // Your logic to handle the creation of a new comment
            return View();
        }

        // POST: /Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comment comment)
        {
            // Your logic to save the new comment to the database
            return RedirectToAction("Index");
        }

        // Other action methods for editing, deleting, etc.
    }
}

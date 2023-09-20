using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
			var model = _context.BlogPosts.ToList();
			return View(model);
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
	}
}
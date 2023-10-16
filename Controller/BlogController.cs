using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
	public class BlogController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly List<BlogPost> _posts = new List<BlogPost>
    {
        new BlogPost { Id = 1, Title = "First post", Content = "Lorem ipsum dolor sit amet" },
        new BlogPost { Id = 2, Title = "Second post", Content = "Consectetur adipiscing elit" },
        new BlogPost { Id = 3, Title = "Third post", Content = "Sed do eiusmod tempor incididunt" }
    };

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
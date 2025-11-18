using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

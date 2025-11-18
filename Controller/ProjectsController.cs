using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

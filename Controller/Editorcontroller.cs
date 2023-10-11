using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class EditorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
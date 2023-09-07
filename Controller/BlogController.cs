using Microsoft.AspNetCore.Mvc;

namespace YourAppName.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

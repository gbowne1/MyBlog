using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        // Placeholder for Login
        public IActionResult Login()
        {
            ViewData["Title"] = "Login Coming Soon";
            return View("Placeholder");
        }

        // Placeholder for Register
        public IActionResult Register()
        {
            ViewData["Title"] = "Registration Coming Soon";
            return View("Placeholder");
        }

        // Placeholder for Manage (signed in)
        public IActionResult Manage()
        {
            ViewData["Title"] = "Dashboard Coming Soon";
            return View("Placeholder");
        }

        // Placeholder for Logout (This needs to be a POST action to work with the form in _Layout)
        [HttpPost]
        public IActionResult Logout()
        {
            // Normally signs out the user. For now, redirect to Home.
            return RedirectToAction("Index", "Home");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class ResetPasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET action for displaying the Reset Password view
        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest("Code is required");
            }

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        // POST action for handling the Reset Password form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Handle the case where the user with the email is not found (optional)
                return RedirectToAction("ForgotPassword", "Account"); // Redirect to forgot password page (optional)
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!resetResult.Succeeded)
            {
                foreach (var error in resetResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            // Password reset successful - you might want to redirect to a confirmation page or login page
            return RedirectToAction("Login", "Account"); // Redirect to the login page
        }
    }
}
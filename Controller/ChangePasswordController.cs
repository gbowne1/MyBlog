using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ChangePasswordController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET action for displaying the Change Password view
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST action for handling the Change Password form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to find the user");
            }

            // Ensure the current password is correct
            var result = await _signInManager.PasswordSignInAsync(user, model.OldPassword, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("OldPassword", "Invalid current password");
                return View(model);
            }

            // Update the password for the user
            var updatePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!updatePasswordResult.Succeeded)
            {
                foreach (var error in updatePasswordResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            // Sign the user out to force a re-login with the new password
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); // Redirect to the login page
        }
    }
}
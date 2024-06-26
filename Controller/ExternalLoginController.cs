using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using System.Linq;

namespace MyBlog.Controllers
{
    public class ExternalLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ExternalLoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Action to remove a linked external login
        public async Task<IActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            if (loginProvider == null || providerKey == null)
            {
                return BadRequest("Missing required parameters");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Unable to find the user");
            }

            if (user.Logins == null)
            {
                return NotFound("No logins found for the user");
            }

            var loginInfo = user.Logins.FirstOrDefault(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
            if (loginInfo == null)
            {
                return NotFound("Login provider not found");
            }

            await _userManager.RemoveLoginAsync(user, loginInfo.LoginProvider, loginInfo.ProviderKey);
            await _userManager.UpdateAsync(user);

            return RedirectToAction("ExternalLogins"); // Redirect to manage logins page
        }

        // Action to initiate external login flow for adding a new provider
        public IActionResult LinkLogin(string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                return BadRequest("Invalid provider");
            }

            var redirectUrl = Url.Action("ExternalLoginCallback", new { returnUrl = HttpContext.Request.Query["returnUrl"] });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties);
        }

        // Callback action to handle successful external login
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login"); // Redirect to login page if info not found
            }

            // Logic for handling retrieved login information (info)
            // This might involve associating the login information with an existing user or creating a new user

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                // Handle the case where the user doesn't exist - create a new user or prompt for additional details
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { ReturnUrl = returnUrl, Info = info });
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl ?? Url.Content("~/")); // Redirect to desired location
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl }); // Handle 2FA if needed
            }

            return BadRequest("Unexpected error"); // Handle other potential errors
        }

        // POST action for handling user creation or additional details after external login (optional)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid model");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return View("ExternalLoginConfirmation", model); // Re-render view if info not found
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError(string.Empty, "Email is required.");
                return View("ExternalLoginConfirmation", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("ExternalLoginConfirmation", model);
                }
            }

            await _userManager.AddLoginAsync(user, info);

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl ?? Url.Content("~/"));
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendCode", new { ReturnUrl = model.ReturnUrl });
            }

            return BadRequest("Unexpected error");
        }
    }
}

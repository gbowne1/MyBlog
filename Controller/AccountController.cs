using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyBlog.Models;
public class AccountController : Controller
{
   private readonly UserManager<IdentityUser> _userManager;
   private readonly SignInManager<IdentityUser> _signInManager;

   public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
   {
       _userManager = userManager;
       _signInManager = signInManager;
   }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Email == null || model.Password == null)
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Your account is not allowed to login at this time.");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account is locked out.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

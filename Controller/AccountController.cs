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
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            switch (result.IsNotAllowed)
            {
                case true:
                    ModelState.AddModelError(string.Empty, "Your account is not allowed to login at this time.");
                    break;
            }

            switch (result.IsLockedOut)
            {
                case true:
                    ModelState.AddModelError(string.Empty, "Your account is locked out.");
                    break;
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

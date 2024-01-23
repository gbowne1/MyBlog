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

   // Actions for login, logout, password management, etc.

   [HttpPost]
   public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }
}

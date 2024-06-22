using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

public class RegisterController : Controller
{
   private readonly UserManager<IdentityUser> _userManager;
   private readonly SignInManager<IdentityUser> _signInManager;

   public RegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
   {
       _userManager = userManager;
       _signInManager = signInManager;
   }

   [HttpGet]
   public IActionResult Register()
   {
       return View();
   }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError("Password", "Password is required.");
            return View(model);
        }

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

   // Other actions for login, logout, password management, etc.
}

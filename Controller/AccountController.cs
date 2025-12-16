using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        // New: Identity Service Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // New: Constructor Injection for Identity Services
        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- REGISTER ACTIONS ---

        // GET: Account/Register (No change to signature, just logic)
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Return the Register view (which we will create next)
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new IdentityUser (the core user object)
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                
                // Attempt to create the user with the given password
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Sign the user in immediately after successful registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    
                    // Redirect to the home page or a success page
                    return RedirectToAction("Index", "Home"); 
                }

                // If creation failed, add errors to ModelState for display
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            // If ModelState is invalid or creation failed, return the model to the view
            return View(model);
        }

        // --- LOGIN ACTIONS ---

        // GET: Account/Login (Needs to accept a returnUrl to handle redirects after login)
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; // Pass return URL to the view
            return View(); // Return the Login view
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (ModelState.IsValid)
            {
                // Attempt to sign in the user
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, 
                    model.Password, 
                    model.RememberMe, 
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect to the original URL if provided, otherwise go home
                    return RedirectToLocal(returnUrl); 
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "User account locked out.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            
            // If we got this far, something failed, re-display form
            return View(model);
        }

        // --- LOGOUT ACTION ---

        // Already correctly defined in your original code
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Actual sign out logic
            return RedirectToAction("Index", "Home");
        }
        
        // --- HELPER METHOD ---
        
        // Helper to safely redirect after login
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        
        // --- PLACEHOLDER ACTIONS (Removed/Updated) ---

        // Placeholder for Manage (signed in) - Keep this for now
        // NOTE: In a full app, this would require [Authorize]
        public IActionResult Manage()
        {
            ViewData["Title"] = "Dashboard Coming Soon";
            return View("Placeholder");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Exercise.Models;

namespace MVC_Exercise.Controllers
{
   
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, DisplayName = vm.UserName, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            var error = "";
            foreach (var e in result.Errors) error += e.Description + "\n";
            ViewBag.ErrorMsg = error;
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                    return LocalRedirect(vm.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("Password", "Invalid Username or password.");
            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SamBookStore.Models.Domain;
using SamBookStore.Models.Domain.ViewModels;

namespace SamBookStore.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {

            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;
        }



        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser() { Email = model.Email, UserName = model.Name, Address = model.Address };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signinManager.PasswordSignInAsync(user, model.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user =await _userManager.FindByEmailAsync(model.Email);
                var result1 = await _signinManager.CheckPasswordSignInAsync(user, model.Password,false);
                var result = await _signinManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Invalid Input");

            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> AddAdmin(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View("Error");
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return View("Error");
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                ModelState.AddModelError("", "The 'Admin' role does not exist.");
                return View("Error");
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError("", "User is already an admin.");
                return View("Error");
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return View(); // Show a success view or message
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Error");
            }
        }
    }
}

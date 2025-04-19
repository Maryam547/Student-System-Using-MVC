using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCD2.Models;
using MVCD2.Repo.Auth;
using MVCD2.ViewModel;

namespace MVCD2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(IAuthRepository authRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state is not valid");
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email.ToUpper());
            if (user == null)
            {
                Console.WriteLine("User not found");
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            Console.WriteLine($"Login result: {result.Succeeded}");

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    return RedirectToAction("Dashboard", "Admin");

                if (roles.Contains("HR"))
                    return RedirectToAction("Dashboard", "HR");

                if (roles.Contains("Instructor"))
                    return RedirectToAction("Courses", "Instructor");

                if (roles.Contains("Student"))
                    return RedirectToAction("MyCourses", "Student");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName, Role = model.Role.ToString()};
            var result = await _authRepository.RegisterUserAsync(user, model.Password,model.Role.ToString());


            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authRepository.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "https://localhost:5031/signin-google")
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (user == null)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new ApplicationUser { UserName = email, Email = email };
                    var result = await _userManager.CreateAsync(user);

                    if (!result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }

                    await _userManager.AddLoginAsync(user, info);
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> ResetPassword()
        //{
        //    var user = await _userManager.FindByEmailAsync("your-email@example.com");
        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var result = await _userManager.ResetPasswordAsync(user, token, "NewPassword123!");

        //        if (result.Succeeded)
        //            return Content("Password reset successful.");
        //        else
        //            return Content("Password reset failed.");
        //    }
        //    return Content("User not found.");
        //}


        
    }
}

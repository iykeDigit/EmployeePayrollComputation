using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayrollComputation.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollComputation.UI.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

      


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user) 
        {
            if (ModelState.IsValid) 
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded) 
                {
                    _logger.LogInformation("User Logged in.");
                    return RedirectToAction("Index", "Home");
                }
                else 
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                    return View();
                }
            }
            return View();
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

             
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM user) 
        {
            
            if (ModelState.IsValid) 
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded) 
                {
                    _logger.LogInformation("User created a new account with password");
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    return Redirect("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout() 
        {
            var user = User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"{user} logged out");
            return RedirectToAction("Login", "Authentication");
        }
    }
}

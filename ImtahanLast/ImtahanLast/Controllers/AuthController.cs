using ImtahanLast.Models;
using ImtahanLast.ViewModels.Autho;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanLast.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);

            if (!register.Email.Contains("@"))
            {
                ModelState.AddModelError("", "Email is valid");
                return View();
            }
            AppUser newUser = new AppUser()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.UserName,
            };
            var result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user;
            if (login.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(login.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(login.UserNameOrEmail);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "user is valid");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

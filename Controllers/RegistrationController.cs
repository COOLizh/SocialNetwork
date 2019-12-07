using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using SocialNetwork.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using SocialNetwork.Services;
using Microsoft.AspNetCore.Authorization;

namespace SocialNetwork.Controllers
{
    public class RegistrationController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Login(LoginViewModel model) { 
        if (ModelState.IsValid) { 
            var result = await _signInManager.PasswordSignInAsync(model.Email,
                model.Password, true, false); 
            if (result.Succeeded) { 
                var usr = await _userManager.FindByEmailAsync(model.Email);
                if(await _userManager.IsEmailConfirmedAsync(usr))
                {
                    return RedirectToAction("Profile", "Account"); 
                }
                else
                {
                    TempData["ErrorMessage"]="Email is not confirmed.";
                    return RedirectToAction("Index", "Home");
                }
            } 
        } 
        TempData["ErrorMessage"]="Incorrect username and / or password";
        return RedirectToAction("Index", "Home");
    }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }
 

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User usr = new User{
                    Name = model.Name, 
                    Surname = model.Surname, 
                    Gender = "Male", 
                    Country = "Russia", 
                    BirthDay = model.BirthDay, 
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                };
                Console.WriteLine(usr);
                var result = await _userManager.CreateAsync(usr, model.Password);
                Console.WriteLine(result);
                if(result.Succeeded){
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(usr);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Registration",
                        new { userId = usr.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Confirm registration by clicking on the link: <a href='{callbackUrl}'>link</a>");
 
                    return Content("To complete the registration, check your email and follow the link provided in the letter");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
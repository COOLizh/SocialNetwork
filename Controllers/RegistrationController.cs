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
                    return RedirectToAction("Index", "Home");
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
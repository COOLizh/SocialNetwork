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
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return View(user);
        }
        [HttpGet]
        public IActionResult Friends(string name)
        {
            List<User> ans = new List<User>();
            List<User> users = _userManager.Users.ToList();
            if (!String.IsNullOrEmpty(name))
            {
                ans = users.Where(p => p.Name.Contains(name)).ToList();
                ans.AddRange(users.Where(p => p.Name.Contains(name)).ToList());
            }
            else{
                return View();
            }
            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = ans,
                Name = name,
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UsersProfile(string id){
            return View(await _userManager.FindByEmailAsync(id));
        }
    }
}
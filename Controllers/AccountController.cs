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
        UsersContext _manager;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UsersContext manager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _manager = manager;
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
                ans.AddRange(users.Where(p => p.Surname.Contains(name)).ToList());
            }

            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = ans,
                Name = name,
                FRiends = _manager.GetFriends(User.Identity.Name),
                Requests = _manager.GetRequests(User.Identity.Name),
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UsersProfile(string id){
            return View(await _userManager.FindByEmailAsync(id));
        }

        [HttpPost]
        public IActionResult FriendRequest(string mail){
            _manager.SendFriendsRequest(User.Identity.Name, mail);
            return RedirectToAction("Friends", "Account");
        }

        [HttpPost]
        public IActionResult AcceptFriendRequest(string email){
            _manager.AcceptFriendsRequest(email, User.Identity.Name);
            return RedirectToAction("Friends", "Account");
        }
    }
}
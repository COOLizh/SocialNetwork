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
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
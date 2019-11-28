using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using SocialNetwork.Models;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.ViewModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace SocialNetwork.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private UsersContext db;
        public RegistrationController(UserManager<User> userManager, UsersContext context)
        {
            _userManager = userManager;
            db = context;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public string Registration(string name, string surname, string email, string password)
        {
            db.Database.EnsureCreated();
            User user = new User{Name = "YA", Surname = "YA", Email = "Kek@mail.ru", Password = "aaa", ConfirmPassword = "aaa", Country = "ZHOPA", Gender = "MALE"};
            db.SUsers.Add(user);
            db.SaveChanges();
            return "Success";
        }
    }
}
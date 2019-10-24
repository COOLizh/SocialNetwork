using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public string Registration(string name, string surname, bool maleGender, string email, string password, string passwordConfirmation, string country){
            
            using (UsersContext db = new UsersContext())
            {
                db.Database.EnsureCreated();    
                User user = new User();
                user.Construct(1, name, surname, maleGender, email, password, country);
                db.Users.Add(user);
                db.SaveChanges();
            }
            return "Вы успешно зарегистрировались, можете войти в свой аккаунт с главной страницы!";
        }
    }
}
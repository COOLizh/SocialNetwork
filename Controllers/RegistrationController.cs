using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

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
                
            

            return "Вы успешно зарегистрировались, можете войти в свой аккаунт с главной страницы!";
        }
    }
}
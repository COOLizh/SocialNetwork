using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace SocialNetwork.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
    }
}
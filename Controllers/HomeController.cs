using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
 
        public string welcome()
        {
            return "Welcome to Csharpstar !!";
        }
    }
}
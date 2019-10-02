using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello World !!";
        }
 
        public string welcome()
        {
            return "Welcome to Csharpstar !!";
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class ErrorController : Controller
    {
        public IActionResult Index(int? code)
        {
            return View(code ?? (int) HttpStatusCode.InternalServerError);
        }
    }
}
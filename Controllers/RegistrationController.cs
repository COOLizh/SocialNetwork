using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using SocialNetwork.Models;
using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Controllers
{
    public class RegistrationController : Controller
    {
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public string Registration(string name, string surname, string gender, string email, string password, string passwordConfirmation, string country){
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
            }

            using (UsersContext db = new UsersContext())
            {
                db.Database.EnsureCreated();    
                User user = new User();

                if ((object)Request.Form["gender"] != null)
                {
                    gender = Request.Form["gender"].ToString();
                }

                user.Construct(name, surname, gender, email, hash, country);
                db.Users.Add(user);
                db.SaveChanges();
            }
            return "You have successfully registered! you can log in to your account from the main page";
        }
    }
}
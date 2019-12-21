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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ExifLib;
using System.Xml.Linq;
using MetOfficeDataPoint;
using MetOfficeDataPoint.Models;
using MetOfficeDataPoint.Models.GeoCoordinate;
using GoogleMaps;


namespace SocialNetwork.Controllers
{
    public class AccountController : Controller
    {
        UsersContext _manager;
        IHostingEnvironment _appEnvironment;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UsersContext manager, IHostingEnvironment appEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _manager = manager;
            _appEnvironment = appEnv;
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

        [HttpGet]
        public IActionResult Dialogues(){
            UsersListViewModel viewModel = new UsersListViewModel{
                FRiends = _manager.GetFriends(User.Identity.Name),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file != null)
            {
                // путь к папке Files
                User usr = await _userManager.FindByEmailAsync(User.Identity.Name);
                string fileName = usr.Email + "_avatar";
                string path = "/photos/" + fileName + ".png";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                usr.Photo = path;
                
                var pc = new PhotoCoordinatesModel();
                try{
                    using (var reader = new ExifReader(_appEnvironment.WebRootPath + path))
                    {
                        pc.Lat = reader.GetLatitude();
                        pc.Lon = reader.GetLongitude();
                    }
                    double lat = 0, lon = 0;
                    if (pc.Lat.HasValue)
                        lat = pc.Lat.Value;
                    else
                    {
                        await _userManager.UpdateAsync(usr);
                        return RedirectToAction("Profile", "Account");
                    }
                    if (pc.Lon.HasValue)
                        lon = pc.Lon.Value;
                    else
                    {
                        await _userManager.UpdateAsync(usr);
                        return RedirectToAction("Profile", "Account");
                    }
                    usr.PhotoLon = lon;
                    usr.PhotoLat = lat;
                    //Console.WriteLine(GetAddress(pc.Lat, pc.Lon));
                }
                catch(ExifLibException exifex)
                {
                    pc.Error = exifex.Message;
                }

                await _userManager.UpdateAsync(usr);
            }
 
            return RedirectToAction("Profile", "Account");
        }

        public IActionResult StartDialogue(string To, string From){
            if(_manager.isDialogueExists(_manager.getDialogueId(From, To))){
                return RedirectToAction("Dialogue", "Account", new { ID = _manager.getDialogueId(From, To)});
            }
            return RedirectToAction("Dialogue", "Account", new { ID = _manager.getDialogueId(To, From)});
        }
        
        [HttpGet]
        public IActionResult Dialogue(string ID){
            DialogueViewModel viewModel = new DialogueViewModel();
            viewModel.messages = _manager.GetDialogue(ID);
            viewModel.id = ID;
            return View(viewModel);
        }

        [HttpPost]
        public void SendMessage(string id, string msg){
            _manager.SendMessage(msg, id, User.Identity.Name);
        }

        [HttpGet]
        public async Task<IActionResult> Map()
        {
            return View(await _userManager.FindByEmailAsync(User.Identity.Name));
        }

        /*
        private string GetAddress(double? latitude, double? longitude)
        {
            double lat = 0, lon = 0;
            if (latitude.HasValue)
                lat = latitude.Value;
            else
                return "";
            if (longitude.HasValue)
                lon = longitude.Value;
            else
                return "";
            MetOfficeDataPointClient client = new MetOfficeDataPointClient("b2d92608-86fe-461e-8717-25e269600438");
            GeoCoordinate coordinate = new GeoCoordinate(lat, lon);
            Location location = client.GetClosestSite(coordinate).Result;
            return location.Name;
        }  */

        
    }
}
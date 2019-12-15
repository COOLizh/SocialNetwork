using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string Name { get; set; }
    }
}
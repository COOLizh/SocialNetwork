using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.ViewModels
{
    public class DialogueViewModel
    {
        public List<Message> messages {get; set;}
        public string id{get; set;}
    }
}
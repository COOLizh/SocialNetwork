using System;

namespace SocialNetwork.Models
{
    public class Message
    {
        public string Id {get; set;}
        public string DialogId {get; set;}
        public string From {get; set;}
        public string Text {get; set;}
        public DateTime Time{get; set;}
    }
}
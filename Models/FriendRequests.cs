namespace SocialNetwork.Models
{
    public class FriendRequests
    {
        public string Id {get; set;}
        public string From{get; set;}
        public string To{get; set;}
        public bool isConfirmed{get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models;

namespace SocialNetwork.Hubs
{
    public class RuHub : Hub
    {
        public async Task Send(string chatId, string person, string message)
        {
            await Clients.Group(chatId).SendAsync("Receive", person, message);
        }
 
        public void JoinGroup(string groupId)
        {
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupId);
        }
    }
}
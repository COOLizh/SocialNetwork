using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models;

namespace SocialNetwork.Hubs
{
    public class UsersHub : Hub
    {
        public async Task Send(string groupname, string post, string nickName, int userId)
        {
            await Clients.Group(groupname).SendAsync("Receive", post, nickName, userId);
        }
 
        public void JoinGroup(string groupId)
        {
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupId);
        }
    }
}
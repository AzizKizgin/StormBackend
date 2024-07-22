using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StormBackend.Models;

namespace StormBackend.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(int groupId, string user, Message message)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToChat(int chatId, Message message)
        {
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
        }
        
        public async Task SendMessageToUser(string userId, Message message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
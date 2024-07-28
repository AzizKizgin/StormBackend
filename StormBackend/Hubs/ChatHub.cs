using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StormBackend.Dtos.Message;
using StormBackend.Models;

namespace StormBackend.SignalR
{
    public class ChatHub : Hub
    {
        [HubMethodName("sendMessage")]
        public async Task SendMessage(string chat, string message)
        {
            await Clients.Group(chat).SendAsync("ReceiveMessage", message);
        }

        [HubMethodName("joinRoom")]
        public async Task JoinRoom(string chat)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chat);
        }

        [HubMethodName("leaveRoom")]
        public async Task LeaveRoom(string chat)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chat);
        }

        [HubMethodName("readMessage")]
        public async Task ReadMessage(string chat, string userId)
        {
            await Clients.Group(chat).SendAsync("RecieveRead", userId);
        }


    }
}
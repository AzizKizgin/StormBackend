using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Chat;

namespace StormBackend.Services.Contacts
{
    public interface IChatService
    {
        Task DeleteChat(string userId, int chatId);
        Task<List<ChatDto>> GetChats(string userId);
        Task<ChatDto> GetChat(string userId, string contactUserId);
        Task MuteChat(string userId, int chatId);
        Task PinChat(string userId, int chatId);
        Task ArchiveChat(string userId, int chatId);
    }
}
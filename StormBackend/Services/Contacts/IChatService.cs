using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Chat;
using StormBackend.Dtos.Message;

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
        Task SendMessage(string userId, string contactUserId, MessageDto message);
        Task SendMessage(string userId, int chatId, MessageDto message);
        Task DeleteMessage(string userId, int messageId);
        Task EditMessage(string userId, int messageId, string newContent);
        Task ReactToMessage(string userId, int messageId, string emoji);
        Task UnreactToMessage(string userId, int messageId);
        Task ReadMessages(string userId, int chatId);
    }
}
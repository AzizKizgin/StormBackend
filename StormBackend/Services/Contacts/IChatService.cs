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
        Task<MessageDto> SendMessage(string userId, string contactUserId, CreateMessageDto message);
        Task<MessageDto> SendMessage(string userId, int chatId, CreateMessageDto message);
        Task<MessageDto> DeleteMessage(string userId, int messageId);
        Task<MessageDto> EditMessage(string userId, int messageId, string newContent);
        Task<MessageDto> ReactToMessage(string userId, int messageId, string emoji);
        Task<MessageDto> UnreactToMessage(string userId, int messageId);
        Task<List<MessageDto>> ReadMessages(string userId, int chatId);
    }
}
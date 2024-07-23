using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IMessageRepository: IRepositoryBase<Message>  
    {
        Task<List<Message>> GetMessagesAsync(string chatId, bool trackChanges);
        Task<Message> GetMessageAsync(int messageId, string userId, bool trackChanges);
        Task<List<Message>> GetUnreadMessageAsync(string chatId, string userId, bool trackChanges);
        void CreateMessage(Message message);
        void DeleteMessage(Message message);
        void UpdateMessage(Message message);
    }
}
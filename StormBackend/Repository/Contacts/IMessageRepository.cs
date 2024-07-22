using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IMessageRepository: IRepositoryBase<Message>  
    {
        Task<Message> GetMessageAsync(int messageId, string userId, bool trackChanges);
        void CreateMessage(Message message);
        void DeleteMessage(Message message);
        void UpdateMessage(Message message);
    }
}
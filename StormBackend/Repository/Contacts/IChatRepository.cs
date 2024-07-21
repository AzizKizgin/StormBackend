using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IChatRepository: IRepositoryBase<Chat>
    {
        Task<List<Chat>> GetChatsAsync(string userId, bool trackChanges);
        Task<Chat> GetChatAsync(string userId, string targetId, bool trackChanges);
        Task<Chat> GetChatByIdAsync(int chatId, bool trackChanges);
        void CreateChat(Chat chat);
        void UpdateChat(Chat chat);
        void DeleteChat(Chat chat);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StormBackend.Data;
using StormBackend.Models;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(AppDBContext context) : base(context)
        {
        }
        public void CreateChat(Chat chat)
        {
            Create(chat);
        }

        public void DeleteChat(Chat chat)
        {
            Delete(chat);
        }

        public Task<Chat> GetChatByTargetIdAsync(string userId, string targetId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Members.Any(m => m.UserId == userId) && c.Members.Any(m => m.UserId == targetId), trackChanges)
                .Include(c => c.Members)
                .ThenInclude(m => m.User)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync();   
            return result;
        }

        public Task<Chat> GetChatByIdAsync(string chatId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Id.ToString() == chatId, trackChanges)
                .Include(c => c.Members)
                .ThenInclude(m => m.User)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<Chat>> GetChatsAsync(string userId, bool trackChanges)
        {
            var chats = FindByCondition(c => c.Members.Any(m => m.UserId == userId), trackChanges)
                .Include(c => c.Messages)
                .Include(c => c.Members)
                .ThenInclude(m => m.User)
                .OrderByDescending(c => c.Messages.Max(m => m.CreatedAt))
                .ToListAsync();
            return chats;
        }

        public void UpdateChat(Chat chat)
        {
            Update(chat);
        }
    }
}
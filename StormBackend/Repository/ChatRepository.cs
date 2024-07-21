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

        public Task<Chat> GetChatAsync(string userId, string targetId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Members.Any(m => m.UserId == userId), trackChanges)
                .Include(c => c.Members)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
                .Include(g => g.Messages)
                .ThenInclude(m => m.Reactions)
                .OrderByDescending(c => c.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp)
                .FirstOrDefaultAsync();   
            return result;
        }

        public Task<Chat> GetChatByIdAsync(int chatId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Id == chatId, trackChanges)
                .Include(c => c.Members)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Sender)
                .Include(g => g.Messages)
                .ThenInclude(m => m.Reactions)
                .OrderByDescending(c => c.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<Chat>> GetChatsAsync(string userId, bool trackChanges)
        {
            var chats = FindByCondition(c => c.Members.Any(m => m.UserId == userId), trackChanges)
                .OrderByDescending(c => c.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp)
                .Include(c => c.Members)
                .ToListAsync();

            return chats;
        }

        public void UpdateChat(Chat chat)
        {
            Update(chat);
        }
    }
}
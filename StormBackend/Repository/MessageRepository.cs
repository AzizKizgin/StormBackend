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
    public class MessageRepository: RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(AppDBContext context) : base(context)
        {
        }

        public void CreateMessage(Message message)
        {
            Create(message);
        }

        public void DeleteMessage(Message message)
        {
            Delete(message);
        }

        public Task<Message> GetMessageAsync(int messageId, string userId, bool trackChanges)
        {
            var result = FindByCondition(m => m.Id == messageId && m.SenderId == userId, trackChanges)
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .Include(m => m.Group)
                .Include(m => m.Reactions)
                .ThenInclude(r => r.User)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<Message>> GetMessagesAsync(int chatId, bool trackChanges)
        {
            var result = FindByCondition(m => m.ChatId == chatId, trackChanges)
                .Include(m => m.Sender)
                .Include(m => m.Chat)
                .Include(m => m.Group)
                .Include(m => m.Reactions)
                .ThenInclude(r => r.User)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
            return result;
        }

        public void UpdateMessage(Message message)
        {
            Update(message);
        }
    }
}
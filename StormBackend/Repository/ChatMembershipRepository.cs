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
    public class ChatMembershipRepository: RepositoryBase<ChatMembership>, IChatMembershipRepository
    {
        public ChatMembershipRepository(AppDBContext context) : base(context)
        {
        }

        public void CreateChatMembership(ChatMembership chatMembership)
        {
            Create(chatMembership);
        }

        public void DeleteChatMembership(ChatMembership chatMembership)
        {
            Delete(chatMembership);
        }

        public Task<ChatMembership> GetChatMembershipAsync(string userId, bool trackChanges)
        {
            var result = FindByCondition(c => c.User.Id == userId, trackChanges)
                .Include(c => c.User)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<ChatMembership>> GetChatMembershipsAsync(int chatId, bool trackChanges)
        {
            var result = FindByCondition(c => c.ChatId == chatId, trackChanges)
                .Include(c => c.User)
                .ToListAsync();
            return result;
        }

        public void UpdateChatMembership(ChatMembership chatMembership)
        {
            Update(chatMembership);
        }
    }
}
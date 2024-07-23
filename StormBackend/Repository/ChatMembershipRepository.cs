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

        public void CreateChatMember(ChatMembership chatMembership)
        {
            Create(chatMembership);
        }

        public void DeleteChatMember(ChatMembership chatMembership)
        {
            Delete(chatMembership);
        }

        public Task<ChatMembership> GetChatMemberByUserIdAsync(string chatId, string userId, bool trackChanges)
        {
            var chatMember = FindByCondition(c => c.ChatId.ToString().Equals(chatId) && c.UserId.Equals(userId), trackChanges)
                .SingleOrDefaultAsync();
            return chatMember;
        }

        public Task<List<ChatMembership>> GetChatMembersAsync(string chatId, bool trackChanges)
        {
            var chatMembers = FindByCondition(c => c.ChatId.ToString().Equals(chatId), trackChanges)
                .ToListAsync();
            return chatMembers;
        }

        public void UpdateChatMember(ChatMembership chatMembership)
        {
            Update(chatMembership);
        }
    }
}
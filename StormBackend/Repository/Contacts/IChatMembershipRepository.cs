using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IChatMembershipRepository: IRepositoryBase<ChatMembership>
    {
        Task <List<ChatMembership>> GetChatMembersAsync(int chatId, bool trackChanges);
        Task <ChatMembership> GetChatMemberByUserIdAsync(int chatId, string userId, bool trackChanges);
        void CreateChatMember(ChatMembership chatMembership);
        void UpdateChatMember(ChatMembership chatMembership);
        void DeleteChatMember(ChatMembership chatMembership);
    }
}
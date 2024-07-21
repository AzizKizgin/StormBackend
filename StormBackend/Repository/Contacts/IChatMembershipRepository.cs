using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IChatMembershipRepository: IRepositoryBase<ChatMembership>
    {
        Task<List<ChatMembership>> GetChatMembershipsAsync(int chatId, bool trackChanges);
        Task<ChatMembership> GetChatMembershipAsync(string userId, bool trackChanges);
        void CreateChatMembership(ChatMembership chatMembership);
        void UpdateChatMembership(ChatMembership chatMembership);
        void DeleteChatMembership(ChatMembership chatMembership);
    }
}
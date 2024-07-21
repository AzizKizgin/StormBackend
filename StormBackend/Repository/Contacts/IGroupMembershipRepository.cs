using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IGroupMembershipRepository: IRepositoryBase<GroupMembership>
    {
            Task<List<GroupMembership>> GetGroupMembershipsAsync(string userId, bool trackChanges);
            Task<GroupMembership> GetGroupMembershipAsync(int groupId, string userId, bool trackChanges);
            void CreateGroupMembership(GroupMembership groupMembership);
            void DeleteGroupMembership(GroupMembership groupMembership); 
            void UpdateGroupMembership(GroupMembership groupMembership);  
    }
}
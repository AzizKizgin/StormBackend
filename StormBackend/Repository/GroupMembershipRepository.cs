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
    public class GroupMembershipRepository: RepositoryBase<GroupMembership>, IGroupMembershipRepository
    {
        public GroupMembershipRepository(AppDBContext context) : base(context)
        {
        }

        public void CreateGroupMembership(GroupMembership groupMembership)
        {
            Create(groupMembership);
        }

        public void DeleteGroupMembership(GroupMembership groupMembership)
        {
            Delete(groupMembership);
        }

        public Task<GroupMembership> GetGroupMembershipAsync(int groupId, string userId, bool trackChanges)
        {
            var result = FindByCondition(gm => gm.GroupId == groupId && gm.UserId == userId, trackChanges)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<GroupMembership>> GetGroupMembershipsAsync(string userId, bool trackChanges)
        {
            var groupMemberships = FindByCondition(gm => gm.UserId == userId, trackChanges)
                .ToListAsync();
            return groupMemberships;
        }

        public void UpdateGroupMembership(GroupMembership groupMembership)
        {
            Update(groupMembership);
        }
    }
}
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
    public class GroupRepository: RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(AppDBContext context) : base(context)
        {
        }
        public void CreateGroup(Group group)
        {
            Create(group);
        }

        public void DeleteGroup(Group group)
        {
            Delete(group);
        }

        public Task<Group> GetGroupAsync(int groupId, bool trackChanges)
        {
            var result = FindByCondition(g => g.Id == groupId, trackChanges)
                .Include(g => g.Members)
                .Include(g => g.Messages)
                .ThenInclude(m => m.Sender)
                .Include(g => g.Messages)
                .ThenInclude(m => m.Reactions)
                .OrderByDescending(g => g.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<Group>> GetGroupsAsync(string userId, bool trackChanges)
        {
            var groups = FindByCondition(g => g.Members.Any(m => m.UserId == userId), trackChanges)
                .OrderByDescending(g => g.Messages.OrderByDescending(m => m.Timestamp).FirstOrDefault().Timestamp)
                .Include(g => g.Members)
                .ToListAsync();
            return groups;
        }

        public void UpdateGroup(Group group)
        {
            Update(group);
        }
    }   
}
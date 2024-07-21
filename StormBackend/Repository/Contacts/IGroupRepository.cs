using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IGroupRepository: IRepositoryBase<Group>
    {
        Task<List<Group>> GetGroupsAsync(string userId, bool trackChanges);
        Task<Group> GetGroupAsync(int groupId, bool trackChanges);
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(Group group);
    }
}
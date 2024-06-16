using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        Task<User> GetUserAsync(string userId, bool trackChanges);
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> DeleteUser(User user);
        Task<IdentityResult> UpdateUser(User user);
    }
}
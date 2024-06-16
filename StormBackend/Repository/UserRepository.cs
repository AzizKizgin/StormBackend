using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StormBackend.Data;
using StormBackend.Models;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(AppDBContext context, UserManager<User> userManager): base(context)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteUser(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<User> GetUserAsync(string userId, bool trackChanges) =>
            await FindByCondition(u => u.Id.Equals(userId), trackChanges).SingleOrDefaultAsync();

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
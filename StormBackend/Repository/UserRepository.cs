using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StormBackend.Data;
using StormBackend.Dtos.User;
using StormBackend.Models;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(AppDBContext context, UserManager<User> userManager, SignInManager<User> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
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

        public Task<User> GetUserByEmailAsync(string email, bool trackChanges)
        {
            return FindByCondition(u => u.Email.Equals(email), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string username, bool trackChanges)
        {
            var user = await FindByCondition(u => u.UserName.Equals(username), trackChanges).SingleOrDefaultAsync();
            return user;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var user = await GetUserByEmailAsync(email, false);
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SearchUsersResult> SearchUsers(SearchUsersQuery query, bool trackChanges)
        {
            var users = FindAll(trackChanges)
                .Where(u => u.UserName.ToLower().Contains(query.Username.ToLower()))
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);

            var result = new SearchUsersResult
            {
                Users = users.ToList(),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalPages = (int)Math.Ceiling(await FindAll(trackChanges).CountAsync() / (double)query.PageSize)
            };
            return result;
        }

        public async Task<IdentityResult> UpdateUser(User user)
        {
            return await _userManager.UpdateAsync(user);
        }    
    }
}
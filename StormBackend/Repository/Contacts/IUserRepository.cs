using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StormBackend.Dtos.User;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IUserRepository: IRepositoryBase<User>
    {
        Task<User> GetUserAsync(string userId, bool trackChanges);
        Task<User> GetUserByEmailAsync(string email, bool trackChanges);
        Task<User> GetUserByUsernameAsync(string username, bool trackChanges);
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> DeleteUser(User user);
        Task<IdentityResult> UpdateUser(User user);
        Task<SignInResult> Login(string email, string password);
        Task Logout();
        Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword);
        Task<SearchUsersResult> SearchUsers(SearchUsersQuery query, bool trackChanges);
    }
}
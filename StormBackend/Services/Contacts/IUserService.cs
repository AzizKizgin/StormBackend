using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StormBackend.Dtos.User;
using StormBackend.Models;

namespace StormBackend.Services.Contacts
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(string id, bool trackChanges);
        Task<UserDto> GetUserByEmail(string email, bool trackChanges);
        Task<UserDto> GetUserByUsername(string username, bool trackChanges);
        Task<UserDto> Login(LoginDto loginInfo);
        Task Logout();
        Task<UserDto> Register(RegisterDto registerInfo);
        Task UpdateUsername(string id, UpdateUsernameDto updateUsernameInfo,bool trackChanges);
        Task UpdateProfilePicture(string id, UpdateProfilePictureDto updateProfilePictureInfo,bool trackChanges);
        Task UpdateUserAbout(string id, UpdateUserAboutDto updateUserAboutInfo,bool trackChanges);
        Task UpdateUserLastSeen(string id, UpdateUserLastSeenDto updateUserLastSeenInfo,bool trackChanges);
        Task DeleteUser(string id);
        Task<string> GenerateToken(User user);
        Task ChangePassword(string id, ChangePasswordDto changePasswordInfo);
        Task<SearchUserDto> SearchUsers(SearchUsersQuery query, bool trackChanges);
    }
}
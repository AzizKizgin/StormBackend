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
        Task<SignInResult> Login(LoginDto loginInfo);
        Task<IdentityResult> Register(RegisterDto registerInfo);
        Task UpdateUsername(string id, UpdateUsernameDto updateUsernameInfo,bool trackChanges);
        Task UpdateProfilePicture(string id, UpdateProfilePictureDto updateProfilePictureInfo,bool trackChanges);
        Task UpdateUserAbout(string id, UpdateUserAboutDto updateUserAboutInfo,bool trackChanges);
        Task DeleteUser(string id);
    }
}
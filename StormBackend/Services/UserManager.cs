using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StormBackend.Dtos.User;
using StormBackend.Models;
using StormBackend.Repository.Contacts;
using StormBackend.Services.Contacts;

namespace StormBackend.Services
{
    public class UserManager: IUserService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _manager.User.GetUserAsync(id, false);
            var result = await _manager.User.DeleteUser(user);
            await _manager.SaveAsync();
        }

        public async Task<UserDto> GetUserById(string id, bool trackChanges)
        {
            var user = await _manager.User.GetUserAsync(id, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByEmail(string email, bool trackChanges)
        {
            var user = await _manager.User.GetUserByEmailAsync(email, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDto>(user);
        }
        public async Task<SignInResult> Login(LoginDto loginInfo)
        {
            var user = await _manager.User.GetUserByEmailAsync(loginInfo.Email, false);
            if (user != null)
            {
                throw new Exception("User with this email does exist");
            }
            var result = await _manager.User.Login(loginInfo.Email, loginInfo.Password);
            if (result == SignInResult.Failed)
            {
                throw new Exception("Failed to login user");
            }
            return result;
        }

        public async Task<IdentityResult> Register(RegisterDto registerInfo)
        {
            var user = _mapper.Map<User>(registerInfo);
            var result = await _manager.User.CreateUser(user, registerInfo.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            return result;
        }

        public async Task UpdateProfilePicture(string id, UpdateProfilePictureDto updateProfilePictureInfo, bool trackChanges)
        {
            var user = await _manager.User.GetUserAsync(id, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.ProfilePicture = Convert.FromBase64String(updateProfilePictureInfo.ProfilePicture);
            var result = await _manager.User.UpdateUser(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update profile picture");
            }
            await _manager.SaveAsync();
        }

        public async Task UpdateUserAbout(string id, UpdateUserAboutDto updateUserAboutInfo, bool trackChanges)
        {
            var user = await _manager.User.GetUserAsync(id, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.About = updateUserAboutInfo.About;
            var result = await _manager.User.UpdateUser(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user about");
            }
            await _manager.SaveAsync();
        }

        public async Task UpdateUsername(string id, UpdateUsernameDto updateUsernameInfo, bool trackChanges)
        {
            var user = await _manager.User.GetUserAsync(id, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Name = updateUsernameInfo.Name;
            var result = _manager.User.UpdateUser(user);
            if (!result.Result.Succeeded)
            {
                throw new Exception("Failed to update username");
            }
            await _manager.SaveAsync();
        }
    }
}
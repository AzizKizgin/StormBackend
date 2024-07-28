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
        private readonly ITokenService _tokenService;

        public UserManager(IRepositoryManager manager, IMapper mapper, ITokenService tokenService)
        {
            _manager = manager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _manager.User.GetUserAsync(id, false);
            var result = await _manager.User.DeleteUser(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete user");
            }
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
        public async Task<UserDto> Login(LoginDto loginInfo)
        {
            var user = await _manager.User.GetUserByEmailAsync(loginInfo.Email, false);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _manager.User.Login(loginInfo.Email, loginInfo.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Your email or password is incorrect");
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await GenerateToken(user);
            return userDto;
        }

        public async Task<UserDto> Register(RegisterDto registerInfo)
        {
            var user = _mapper.Map<User>(registerInfo);
            var result = await _manager.User.CreateUser(user, registerInfo.Password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await GenerateToken(user);
            return userDto;
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
            user.UserName = updateUsernameInfo.Username;
            var result = _manager.User.UpdateUser(user);
            if (!result.Result.Succeeded)
            {
                throw new Exception("Failed to update username");
            }
            await _manager.SaveAsync();
        }

        public Task<string> GenerateToken(User user)
        {
            var token = _tokenService.GenerateToken(user);
            return Task.FromResult(token);
        }

        public async Task ChangePassword(string id, ChangePasswordDto changePasswordInfo)
        {
            var user = await _manager.User.GetUserAsync(id, false);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result =  await _manager.User.ChangePassword(user, changePasswordInfo.OldPassword, changePasswordInfo.NewPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to change password");
            }
        }

        public async Task Logout()
        {
            await _manager.User.Logout();
        }

        public async Task UpdateUserLastSeen(string id, UpdateUserLastSeenDto updateUserLastSeenInfo, bool trackChanges)
        {
            var user = await _manager.User.GetUserAsync(id, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.LastSeen = updateUserLastSeenInfo.LastSeen;
            var result = await _manager.User.UpdateUser(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update last seen");
            }
            await _manager.SaveAsync();
        }

        public async Task<SearchUserDto> SearchUsers(SearchUsersQuery query, bool trackChanges)
        {
            var searchUsersResult = await _manager.User.SearchUsers(query, trackChanges);
            var searchUsers = new SearchUserDto
            {
                Users = _mapper.Map<List<UserDto>>(searchUsersResult.Users),
                Page = searchUsersResult.Page,
                PageSize = searchUsersResult.PageSize,
                TotalPages = searchUsersResult.TotalPages
            };
            return searchUsers;
        }

        public async Task<UserDto> GetUserByUsername(string username, bool trackChanges)
        {
            var user = await _manager.User.GetUserByUsernameAsync(username, trackChanges);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
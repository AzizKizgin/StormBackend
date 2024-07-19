using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StormBackend.Dtos;
using StormBackend.Dtos.User;
using StormBackend.Services.Contacts;

namespace StormBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public UserController(IUserService userService, IContactService contactService)
        {
            _userService = userService;
            _contactService = contactService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var result = await _userService.Login(loginDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return BadRequest("Passwords do not match");
                }
                var result = await _userService.Register(registerDto);
                if (result == null)
                {
                    return BadRequest("Failed to register user");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.Logout();
                var successMessage = new SuccessMessage
                {
                    Message = "User logged out successfully"
                };
                return Ok(successMessage);
            }
            catch 
            {
                return BadRequest("Failed to logout user");
            }
        }

        [Authorize]
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                var user = await _userService.GetUserById(userId, false);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        

        [Authorize]
        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameDto updateUsernameDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _userService.UpdateUsername(userId, updateUsernameDto, true);
                var successMessage = new SuccessMessage
                {
                    Message = "Username updated successfully"
                };
                return Ok(successMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture([FromBody] UpdateProfilePictureDto updateProfilePictureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _userService.UpdateProfilePicture(userId, updateProfilePictureDto, true);
                var successMessage = new SuccessMessage
                {
                    Message = "Profile picture updated successfully"
                };
                return Ok(successMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-user-about")]
        public async Task<IActionResult> UpdateUserAbout([FromBody] UpdateUserAboutDto updateUserAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _userService.UpdateUserAbout(userId, updateUserAboutDto, true);
                var successMessage = new SuccessMessage
                {
                    Message = "User about updated successfully"
                };
                return Ok(successMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _userService.DeleteUser(userId);
                var successMessage = new SuccessMessage
                {
                    Message = "User deleted successfully"
                };
                return Ok(successMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-user-last-seen")]
        public async Task<IActionResult> UpdateUserLastSeen([FromBody] UpdateUserLastSeenDto updateUserLastSeenDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _userService.UpdateUserLastSeen(userId, updateUserLastSeenDto, true);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("search-users")]
        public async Task<IActionResult> SearchUsers([FromQuery] SearchUsersQuery searchUsersDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                var userContact = await _contactService.GetContacts(userId);
                var users = await _userService.SearchUsers(searchUsersDto, true);

                foreach (var user in users.Users)
                {
                    user.IsContactOfCurrentUser = userContact.Any(c => c.ContactUser.Id == user.Id);
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Invalid model object");
                }
                var user = await _userService.GetUserById(id, true);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
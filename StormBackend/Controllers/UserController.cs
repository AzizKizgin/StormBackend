using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StormBackend.Dtos.User;
using StormBackend.Services.Contacts;

namespace StormBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
                var result = await _userService.Register(registerDto);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("User registered successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-username/{id}")]
        public async Task<IActionResult> UpdateUsername(string id, [FromBody] UpdateUsernameDto updateUsernameDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _userService.UpdateUsername(id, updateUsernameDto, true);
                return Ok("Username updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-profile-picture/{id}")]
        public async Task<IActionResult> UpdateProfilePicture(string id, [FromBody] UpdateProfilePictureDto updateProfilePictureDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _userService.UpdateProfilePicture(id, updateProfilePictureDto, true);
                return Ok("Profile picture updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("update-user-about/{id}")]
        public async Task<IActionResult> UpdateUserAbout(string id, [FromBody] UpdateUserAboutDto updateUserAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _userService.UpdateUserAbout(id, updateUserAboutDto, true);
                return Ok("User about updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
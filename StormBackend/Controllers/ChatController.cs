using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StormBackend.Dtos;
using StormBackend.Dtos.EmojiReaction;
using StormBackend.Dtos.Message;
using StormBackend.Services.Contacts;
using StormBackend.SignalR;

namespace StormBackend.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController: ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }
            var userId =User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
                
            if (message.ChatId != null)
            {
                var result = await _chatService.SendMessage(userId, message.ChatId, message);
                return Ok(result);
              
            }
            return BadRequest("Invalid message");
        }
    
        [HttpDelete("delete/{messageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = await _chatService.DeleteMessage(userId, messageId);
            return Ok();
        }

        [HttpPut("edit/{messageId}")]
        [Authorize]
        public async Task<IActionResult> EditMessage(int messageId, [FromBody] EditMessageDto newContent)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = await _chatService.EditMessage(userId, messageId, newContent.Content);
            return Ok(result);
        }

        [HttpPost("react/{messageId}")]
        [Authorize]
        public async Task<IActionResult> ReactToMessage(int messageId, [FromBody] UpsertEmojiReactionDto emojiDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = await _chatService.ReactToMessage(userId, messageId, emojiDto.Emoji);
            return Ok(result);
        }

        [HttpDelete("unreact/{messageId}")]
        [Authorize]
        public async Task<IActionResult> UnreactToMessage(int messageId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = await _chatService.UnreactToMessage(userId, messageId);
            return Ok(result);
        }

        [HttpPost("read/{chatId}")]
        [Authorize]
        public async Task<IActionResult> ReadMessages(string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            await _chatService.ReadMessages(userId, chatId);
            var successMessage = new SuccessMessage
            {
                Message = "Messages read successfully"
            };
            return Ok(successMessage);
        }

        [HttpPost("mute/{chatId}")]
        [Authorize]
        public async Task<IActionResult> MuteChat(string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            await _chatService.MuteChat(userId, chatId);
            return Ok();
        }

        [HttpPost("pin/{chatId}")]
        [Authorize]
        public async Task<IActionResult> PinChat(string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            await _chatService.PinChat(userId, chatId);
            return Ok();
        }

        [HttpPost("archive/{chatId}")]
        [Authorize]
        public async Task<IActionResult> ArchiveChat(string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            await _chatService.ArchiveChat(userId, chatId);
            return Ok();
        }

        [HttpGet("get/{chatId}")]
        [Authorize]
        public async Task<IActionResult> GetChat(string chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = 
                await _chatService.GetChatById(userId, chatId) 
                ?? await _chatService.GetChatByContactId(userId, chatId);

            if (result == null)
            {
                return BadRequest("Chat not found");
            }
            return Ok(result);
        }

        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetChats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var result = await _chatService.GetChats(userId);
            return Ok(result);
        }
    }
}
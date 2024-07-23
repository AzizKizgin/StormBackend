using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatController(IChatService chatService, IHubContext<ChatHub> hubContext)
        {
            _chatService = chatService;
            _hubContext = hubContext;
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
                
            
            MessageDto result;
            if (message.ChatId != null)
            {
                result = await _chatService.SendMessage(userId, message.ChatId.Value, message);
                await _hubContext.Clients.Group(message.ChatId.Value.ToString()).SendAsync("ReceiveMessage", result);
              
            }
            else if (message.GroupId != null)
            {
                result = null;
            }
            else
            {
                result = await _chatService.SendMessage(userId, message.ReceiverId, message);
                await _hubContext.Clients.User(message.ReceiverId).SendAsync("ReceiveMessage", result);
            }
            return Ok(result);
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
            await _hubContext.Clients.Group(result.ChatId.ToString()).SendAsync("ReceiveMessage", result);
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
            await _hubContext.Clients.Group(result.ChatId.ToString()).SendAsync("ReceiveMessage", result);
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
            await _hubContext.Clients.Group(result.ChatId.ToString()).SendAsync("ReceiveMessage", result);
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
            await _hubContext.Clients.Group(result.ChatId.ToString()).SendAsync("ReceiveMessage", result);
            return Ok(result);
        }

        [HttpPost("read/{chatId}")]
        [Authorize]
        public async Task<IActionResult> ReadMessages(int chatId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            await _chatService.ReadMessages(userId, chatId);
            await _hubContext.Clients.Group(chatId.ToString()).SendAsync("ReadMessage", userId);
            return Ok();
        }

        [HttpPost("mute/{chatId}")]
        [Authorize]
        public async Task<IActionResult> MuteChat(int chatId)
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
        public async Task<IActionResult> PinChat(int chatId)
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
        public async Task<IActionResult> ArchiveChat(int chatId)
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
            var result = await _chatService.GetChat(userId, chatId);
            return Ok(result);
        }

        [HttpGet("get")]
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
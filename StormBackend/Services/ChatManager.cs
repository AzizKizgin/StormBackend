using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.Chat;
using StormBackend.Repository.Contacts;
using StormBackend.Services.Contacts;

namespace StormBackend.Services
{
    public class ChatManager: IChatService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ChatManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task ArchiveChat(string userId, int chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, false);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsArchived = !chatMember.IsArchived;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task DeleteChat(string userId, int chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, false);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            _manager.ChatMembership.DeleteChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task<ChatDto> GetChat(string userId, string contactUserId)
        {
            var chat = await _manager.Chat.GetChatAsync(userId, contactUserId, false);
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<List<ChatDto>> GetChats(string userId)
        {
            var chats = await _manager.Chat.GetChatsAsync(userId, false);
            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task MuteChat(string userId, int chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, false);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsMuted = !chatMember.IsMuted;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task PinChat(string userId, int chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, false);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsPinned = !chatMember.IsPinned;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }
    }
}
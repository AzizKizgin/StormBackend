using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.Chat;
using StormBackend.Dtos.Message;
using StormBackend.Models;
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

        public async Task ArchiveChat(string userId, string chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, true);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsArchived = !chatMember.IsArchived;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task DeleteChat(string userId, string chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, false);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            _manager.ChatMembership.DeleteChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task<MessageDto> DeleteMessage(string userId, int messageId)
        {
            var message = await _manager.Message.GetMessageAsync(messageId, userId, true);
            if (message == null)
            {
                throw new Exception("Message not found");
            }
            _manager.Message.DeleteMessage(message);
            await _manager.SaveAsync();
            var messageDto = _mapper.Map<MessageDto>(message);
            messageDto.Type = MessageType.Delete;
            return messageDto;
        }

        public async Task<MessageDto> EditMessage(string userId, int messageId, string newContent)
        {
            var message = await _manager.Message.GetMessageAsync(messageId, userId, true);
            if (message == null)
            {
                throw new Exception("Message not found");
            }
            message.Content = newContent;
            message.EditedAt = DateTime.Now;
            _manager.Message.UpdateMessage(message);
            await _manager.SaveAsync();
            var messageDto = _mapper.Map<MessageDto>(message);
            messageDto.Type = MessageType.Edit;
            return messageDto;
        }

        public async Task<ChatDto> GetChatByContactId(string userId, string contactUserId)
        {
            var chat = await _manager.Chat.GetChatByTargetIdAsync(userId, contactUserId, false);
            if (chat != null)
            {
                var chatDto = _mapper.Map<ChatDto>(chat);
                return chatDto;
            }
           
            var newChat = new Chat
            {
                Id = Guid.NewGuid(),
                Members = new List<ChatMembership>
                {
                    new ChatMembership { UserId = userId },
                    new ChatMembership { UserId = contactUserId }
                },
                Messages = new List<Message>()
            };
            _manager.Chat.CreateChat(newChat);
            await _manager.SaveAsync();
            var existingChat = await _manager.Chat.GetChatByIdAsync(newChat.Id.ToString(), false);
            var newChatDto = _mapper.Map<ChatDto>(existingChat);
            return newChatDto;
        }

        public async Task<ChatDto> GetChatById(string userId, string chatId)
        {
            var chat = await _manager.Chat.GetChatByIdAsync(chatId, false);
            if (chat != null)
            {
                var messages = await _manager.Message.GetMessagesAsync(chatId, false);
                chat.Messages = messages;
                var chatDto = _mapper.Map<ChatDto>(chat);
                return chatDto;
            }
            return null;
        }

        public async Task<List<ChatDto>> GetChats(string userId)
        {
            var chats = await _manager.Chat.GetChatsAsync(userId, false);
            var chatsDto = _mapper.Map<List<ChatDto>>(chats);
            foreach (var chat in chatsDto)
            {
                var messages = await _manager.Message.GetMessagesAsync(chat.Id.ToString(), false);
                chat.Messages = _mapper.Map<List<MessageDto>>(messages);
            }
          
            return chatsDto;
        }

        public async Task MuteChat(string userId, string chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, true);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsMuted = !chatMember.IsMuted;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task PinChat(string userId, string chatId)
        {
            var chatMember = await _manager.ChatMembership.GetChatMemberByUserIdAsync(chatId, userId, true);
            if (chatMember == null)
            {
                throw new Exception("Chat member not found");
            }
            chatMember.IsPinned = !chatMember.IsPinned;
            _manager.ChatMembership.UpdateChatMember(chatMember);
            await _manager.SaveAsync();
        }

        public async Task<MessageDto> ReactToMessage(string userId, int messageId, string emoji)
        {
            var message = await _manager.Message.GetMessageAsync(messageId, userId, true);
            if (message == null)
            {
                throw new Exception("Message not found");
            }
            var reaction = new EmojiReaction
            {
                MessageId = messageId,
                UserId = userId,
                Emoji = emoji
            };
            var existingReaction = await _manager.EmojiReaction.GetEmoji(messageId, userId);
            if (existingReaction != null)
            {
                _manager.EmojiReaction.DeleteEmoji(existingReaction);
                message.Reactions.Remove(existingReaction);
            }
            _manager.EmojiReaction.CreateEmoji(reaction);
            message.Reactions.Add(reaction);
            _manager.Message.UpdateMessage(message);
            await _manager.SaveAsync();
            var messageDto = _mapper.Map<MessageDto>(message);
            messageDto.Type = MessageType.Reaction;
            return messageDto;
        }

        public async Task ReadMessages(string userId, string chatId)
        {
            var messages = await _manager.Message.GetMessagesAsync(chatId, true);
            foreach (var message in messages)
            {
                if (!message.ReadBy.Contains(userId))
                {
                    message.ReadBy.Add(userId);
                    _manager.Message.UpdateMessage(message);
                }
            }
            await _manager.SaveAsync();
        }

        public async Task<MessageDto> SendMessage(string userId, string chatId, CreateMessageDto message)
        {
            var newMessage = new Message
            {
                ChatId = Guid.Parse(chatId),
                Content = message.Content,
                SenderId = userId,
                CreatedAt = DateTime.Now,
                ReadBy = new List<string> { userId }
                
            };
            var chat = await _manager.Chat.GetChatByIdAsync(chatId.ToString(), true);
            if (chat == null)
            {
                throw new Exception("Chat not found");
            }
            chat.Messages.Add(newMessage);
            _manager.Chat.UpdateChat(chat);
            _manager.Message.CreateMessage(newMessage);
            await _manager.SaveAsync();
            var messageDto = _mapper.Map<MessageDto>(newMessage);
            messageDto.Type = MessageType.Add;
            return messageDto;
        }

        public async Task<MessageDto> UnreactToMessage(string userId, int messageId)
        {
            var reaction = await _manager.EmojiReaction.GetEmoji(messageId, userId);
            if (reaction == null)
            {
                throw new Exception("Reaction not found");
            }
            _manager.EmojiReaction.DeleteEmoji(reaction);
            await _manager.SaveAsync();
            var message = await _manager.Message.GetMessageAsync(messageId, userId, false);
            var messageDto = _mapper.Map<MessageDto>(message);
            messageDto.Type = MessageType.Reaction;
            return messageDto;
        }
    }
}
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

        public async Task<MessageDto> DeleteMessage(string userId, int messageId)
        {
            var message = await _manager.Message.GetMessageAsync(messageId, userId, false);
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
            var message = await _manager.Message.GetMessageAsync(messageId, userId, false);
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

        public async Task<ChatDto> GetChat(string userId, string contactUserId)
        {
            var chat = await _manager.Chat.GetChatAsync(userId, contactUserId, false);
            var messages = await _manager.Message.GetMessagesAsync(chat.Id, false);
            chat.Messages = messages;
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

        public async Task<MessageDto> ReactToMessage(string userId, int messageId, string emoji)
        {
            var message = await _manager.Message.GetMessageAsync(messageId, userId, false);
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

        public async Task<List<MessageDto>> ReadMessages(string userId, int chatId)
        {
            var messages = await _manager.Message.GetMessagesAsync(chatId, false);
            foreach (var message in messages)
            {
                if (message.SenderId != userId)
                {
                    message.ReadBy.Add(userId);
                    _manager.Message.UpdateMessage(message);
                }
            }
            await _manager.SaveAsync();
            var messageList = _mapper.Map<List<MessageDto>>(messages);
            foreach (var message in messageList)
            {
                message.Type = MessageType.Read;
            }
            return messageList;
        }

        public async Task<MessageDto> SendMessage(string userId, string contactUserId, CreateMessageDto message)
        {
            var chat = await _manager.Chat.GetChatAsync(userId, contactUserId, false);
            if (chat == null)
            {
                chat = new Chat
                {
                    Members = new List<ChatMembership>
                    {
                        new ChatMembership
                        {
                            UserId = userId
                        },
                        new ChatMembership
                        {
                            UserId = contactUserId
                        }
                    }
                };
                _manager.Chat.CreateChat(chat);
                await _manager.SaveAsync();
            }
            var newMessage = new Message
            {
                ChatId = chat.Id,
                Content = message.Content,
                SenderId = userId,
                CreatedAt = DateTime.Now
            };
            _manager.Message.CreateMessage(newMessage);
            await _manager.SaveAsync();
            var messageDto = _mapper.Map<MessageDto>(newMessage);
            messageDto.Type = MessageType.Add;
            return messageDto;
        }

        public async Task<MessageDto> SendMessage(string userId, int chatId, CreateMessageDto message)
        {
            var newMessage = new Message
            {
                ChatId = chatId,
                Content = message.Content,
                SenderId = userId,
                CreatedAt = DateTime.Now
            };
            var chat = await _manager.Chat.GetChatByIdAsync(chatId, false);
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
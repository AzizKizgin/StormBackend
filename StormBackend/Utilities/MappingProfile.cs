using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.Chat;
using StormBackend.Dtos.Contact;
using StormBackend.Dtos.User;
using StormBackend.Dtos.ChatMember;
using StormBackend.Models;
using StormBackend.Dtos.Message;
using StormBackend.Dtos.EmojiReaction;

namespace StormBackend.Utilities
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
            CreateMap<CreateContactDto, Contact>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<Contact, ContactDto>();
            CreateMap<ChatMembership, ChatMemberDto>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.UserId.ToString()));
            CreateMap<Chat, ChatDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId.ToString()));
            CreateMap<CreateMessageDto, Message>()
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => Guid.Parse(src.ChatId)));
            CreateMap<EmojiReaction, EmojiReactionDto>();
        }
    }
}
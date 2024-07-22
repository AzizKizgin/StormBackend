using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.ChatMember;
using StormBackend.Dtos.Message;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.Chat
{
    public class ChatDto
    {
        public int Id { get; set; }
        public List<ChatMemberDto> Members { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.ChatMember
{
    public class ChatMemberDto
    {
        public UserDto User { get; set; }
        public int ChatId { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsMuted { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
    }
}
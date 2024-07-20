using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.EmojiReaction;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public UserDto Sender { get; set; }
        public int? ChatId { get; set; }
        public int? GroupId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public List<byte[]> Media { get; set; }
        public List<string> ReadBy { get; set; }
        public List<EmojiReactionDto> Reactions { get; set; }
    }
}
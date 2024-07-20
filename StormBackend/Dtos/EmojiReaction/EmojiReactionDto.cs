using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.EmojiReaction
{
    public class EmojiReactionDto
    {
        public int Id { get; set; }
        public string Emoji { get; set; }
        public int MessageId { get; set; }
        public UserDto Sender { get; set; }
    }
}
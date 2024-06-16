using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public int? ChatId { get; set; } // Nullable for group messages
        public Chat Chat { get; set; }
        public int? GroupId { get; set; } // Nullable for private messages
        public Group Group { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public List<byte[]> Media { get; set; }
        public List<string> ReadBy { get; set; }
        public List<EmojiReaction> Reactions { get; set; }
    }

    public class EmojiReaction
    {
        public string UserId { get; set; }
        public string Emoji { get; set; }
    }
}
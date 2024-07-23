using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Chat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<ChatMembership> Members { get; set; }
        public List<Message> Messages { get; set; }
    }
}
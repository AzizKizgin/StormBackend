using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<ChatMembership> Members { get; set; }
        public List<Message> Messages { get; set; }
        public Message LastMessage { get; set; }
    }
}
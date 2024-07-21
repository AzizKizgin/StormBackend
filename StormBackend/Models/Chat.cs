using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public ChatMembership ChatMember1 { get; set; }
        public ChatMembership ChatMember2 { get; set; }
        public List<Message> Messages { get; set; }
    }
}
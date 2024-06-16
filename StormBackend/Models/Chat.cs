using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string? User1Id { get; set; }
        public User User1 { get; set; }
        public string? User2Id { get; set; }
        public User User2 { get; set; }
        public List<Message> Messages { get; set; }
    }
}
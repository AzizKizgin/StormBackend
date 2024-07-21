using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class ChatMembership
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime JoinedAt { get; set; } 
        public DateTime? LastDeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsMuted { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
    }
}
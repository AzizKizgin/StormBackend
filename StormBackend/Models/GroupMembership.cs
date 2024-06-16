using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class GroupMembership
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }
        public bool IsMuted { get; set; }
    }
}
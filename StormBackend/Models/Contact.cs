using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string?  UserId { get; set; }
        public User User { get; set; }
        public string?  ContactUserId { get; set; }
        public User ContactUser { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime BlockedAt { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsMuted { get; set; }
        public bool IsAccepted { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.Contact
{
    public class ContactDto
    {
        public string ContactId { get; set; }
        public UserDto User { get; set; }
        public ContactUserDto ContactUser { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime BlockedAt { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsMuted { get; set; }
        public bool IsAccepted { get; set; }
    }
}
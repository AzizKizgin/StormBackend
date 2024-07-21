using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StormBackend.Models
{
    public class User: IdentityUser
    {
        public DateTime CreatedAt { get; set; }
        public byte[]? ProfilePicture { get; set; } 
        public string About { get; set; } = "Hey there! I am using Storm";
        public DateTime LastSeen { get; set; }
        public List<ChatMembership> ChatMemberships { get; set; }
        public List<GroupMembership> GroupMemberships { get; set; }
        public List<Contact> Contacts { get; set; }
        
    }
}
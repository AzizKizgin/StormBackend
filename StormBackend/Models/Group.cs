using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupMembership> Members { get; set; }
        public List<Message> Messages { get; set; }
        public string Description { get; set; }
        public byte[] GroupPicture { get; set; }
        public DateTime CreatedAt { get; set; }
     
    }
}
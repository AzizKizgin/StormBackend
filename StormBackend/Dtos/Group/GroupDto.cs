using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.GroupMembership;
using StormBackend.Dtos.Message;

namespace StormBackend.Dtos.Group
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GroupMembershipDto> Members { get; set; }
        public List<MessageDto> Messages { get; set; }
        public string Description { get; set; }
        public byte[] GroupPicture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.GroupMembership
{
    public class GroupMembershipDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public UserDto User { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBanned { get; set; }
        public bool IsMuted { get; set; }
        public bool IsCreator { get; set; }
    }
}
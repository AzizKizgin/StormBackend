using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.Contact
{
    public record ContactUserDto
    {
        public string Username { get; init; }
        public string About { get; init; }
        public DateTime CreatedAt { get; init; }
        public string ProfilePicture { get; init; }
        public bool IsBlocked { get; init; }
        public bool IsMuted { get; init; }
        public bool IsAccepted { get; init;}
    }
}
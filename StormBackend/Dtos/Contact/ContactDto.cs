using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.User;

namespace StormBackend.Dtos.Contact
{
    public class ContactDto
    {
        public string Id { get; set; }
        public UserDto ContactUser { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
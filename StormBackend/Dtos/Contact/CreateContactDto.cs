using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.Contact
{
    public record CreateContactDto
    {
        public string ContactUserName { get; init; }
        public DateTime AddedAt { get; init; } = DateTime.Now;
        public bool IsAccepted { get; init; } = false;
    }
}
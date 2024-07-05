using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.Contact
{
    public record SearchContactsQuery
    {
        public bool? IsBlocked { get; init; }
        public bool? IsMuted { get; init; }
        public bool? IsAccepted { get; init; }
        public string? ContactUserName { get; init; }
    }
}
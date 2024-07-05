using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.Contact
{
    public record CreateContactDto
    {
        public string ContactUserId { get; init; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record UpdateUserAboutDto
    {
        public string About { get; init; }
    }
}
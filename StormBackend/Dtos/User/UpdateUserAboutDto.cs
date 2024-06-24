using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record UpdateUserAboutDto
    {
        [MaxLength(100)]
        public string About { get; init; }
    }
}
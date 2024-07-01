using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record SearchUserDto
    {
        public List<UserDto> Users { get; init; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        
    }
}
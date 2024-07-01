using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record SearchUsersQuery
    {
        public string Username { get; init; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
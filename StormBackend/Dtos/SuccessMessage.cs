using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos
{
    public record SuccessMessage
    {
        public string Message { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}
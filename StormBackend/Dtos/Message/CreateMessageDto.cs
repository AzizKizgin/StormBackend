using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.Message
{
    public class CreateMessageDto
    {
        public string Content { get; set; }
        public string? ReceiverId { get; set; }
        public List<byte[]>? Media { get; set; } = null;
        public int? ChatId { get; set; }
        public int? GroupId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditedAt { get; set; } = null;
    }
}
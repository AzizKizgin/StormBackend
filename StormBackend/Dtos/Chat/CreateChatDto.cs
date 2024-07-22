using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Message;

namespace StormBackend.Dtos.Chat
{
    public class CreateChatDto
    {
        public string ContactUserId { get; set; }
        public CreateMessageDto InitialMessage { get; set; }
    }
}
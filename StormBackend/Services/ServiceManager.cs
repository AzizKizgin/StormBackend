using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Services.Contacts;

namespace StormBackend.Services
{
    public class ServiceManager: IServiceManager
    {
        private readonly IUserService _userService;
        private readonly IContactService _contactService;
        private readonly IChatService _chatService;
        public ServiceManager(IUserService userService, IContactService contactService, IChatService chatService)
        {
            _userService = userService;
            _contactService = contactService;
            _chatService = chatService;
        }
        public IUserService UserService => _userService;
        public IContactService ContactService => _contactService;
        public IChatService ChatService => _chatService;
    }
}
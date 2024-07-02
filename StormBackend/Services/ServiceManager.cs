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
        public ServiceManager(IUserService userService, IContactService contactService)
        {
            _userService = userService;
            _contactService = contactService;
        }
        public IUserService UserService => _userService;
        public IContactService ContactService => _contactService;
    }
}
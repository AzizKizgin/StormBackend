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
        public ServiceManager(IUserService userService)
        {
            _userService = userService;
        }
        public IUserService UserService => _userService;
    }
}
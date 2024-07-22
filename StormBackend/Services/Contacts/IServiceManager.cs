using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Services.Contacts
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IContactService ContactService { get; }
        IChatService ChatService { get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Repository.Contacts
{
    public interface IRepositoryManager
    {
        Task SaveAsync();
    }
}
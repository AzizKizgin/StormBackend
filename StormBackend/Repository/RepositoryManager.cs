using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Data;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly AppDBContext _context;
        private readonly IUserRepository _user;

        public RepositoryManager(AppDBContext context, IUserRepository user)
        {
            _context = context;
            _user = user;
        }

        public IUserRepository User => _user;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
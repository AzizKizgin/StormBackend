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
        private readonly IContactRepository _contact;

        public RepositoryManager(AppDBContext context, IUserRepository user, IContactRepository contact)
        {
            _context = context;
            _user = user;
            _contact = contact;
        }

        public IUserRepository User => _user;

        public IContactRepository Contact => _contact;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
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

        public RepositoryManager(AppDBContext context)
        {
            _context = context;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
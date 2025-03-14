using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StormBackend.Data;
using StormBackend.Dtos.Contact;
using StormBackend.Models;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(AppDBContext context) : base(context)
        {
        }

        public void CreateContact(Contact contact)
        {
            Create(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Delete(contact);
        }

        public async Task<Contact> GetContactAsync(string userId, string contactUserId, bool trackChanges)
        {
            var result = await FindByCondition(c => c.UserId == userId && c.ContactUserId == contactUserId,trackChanges).FirstOrDefaultAsync();
            return result;
        }

        public Task<Contact> GetContactByIdAsync(int contactId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Id == contactId, trackChanges).FirstOrDefaultAsync();
            return result;
        }

        public Task<Contact> GetContactByUserIdAsync(string userId, string targetId, bool trackChanges)
        {
            var result = FindByCondition(c => c.UserId == userId && c.ContactUserId == targetId, trackChanges).FirstOrDefaultAsync();
            return result;
        }

        public Task<List<Contact>> GetContactsAsync(string userId, bool trackChanges)
        {
            var contacts = FindAll(trackChanges)
                .Where(c => c.UserId == userId)
                .Include(c => c.ContactUser)
                .OrderBy(c => c.ContactUser.UserName);
            
            return contacts.ToListAsync();
                
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }
    }
}
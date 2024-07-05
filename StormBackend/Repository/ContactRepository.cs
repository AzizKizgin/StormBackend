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

        public Task<List<Contact>> GetContactsAsync(string userId, SearchContactsQuery query, bool trackChanges)
        {
            var contacts = FindAll(trackChanges).Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(query.ContactUserName))
            {
                contacts = contacts.Where(c => c.ContactUser.UserName == query.ContactUserName);
            }

            if (query.IsAccepted.HasValue && query.IsAccepted == true)
            {
                contacts = contacts.Where(c => c.IsAccepted);
            }

            if (query.IsBlocked.HasValue && query.IsBlocked == true)
            {
                contacts = contacts.Where(c => c.IsBlocked);
            }

            if (query.IsMuted.HasValue && query.IsMuted == true)
            {
                contacts = contacts.Where(c => c.IsMuted);
            }
                
            return contacts.ToListAsync();
                
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }
    }
}
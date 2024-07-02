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

        public async Task<Contact> GetContactAsync(int contactId, bool trackChanges)
        {
            var result = await FindByCondition(c => c.Id == contactId, trackChanges).SingleOrDefaultAsync();
            return result;
        }

        public Task<Contact> GetContactAsync(string contactUserName, bool trackChanges)
        {
            var result = FindByCondition(c => c.ContactUser.UserName == contactUserName, trackChanges).SingleOrDefaultAsync();
            return result;
        }

        public Task<List<Contact>> GetContactsAsync(GetContactsQuery query, bool trackChanges)
        {
            var contacts = FindAll(trackChanges);

            if (!string.IsNullOrEmpty(query.UserId))
            {
                contacts = contacts.Where(c => c.UserId == query.UserId);
            }

            if (!string.IsNullOrEmpty(query.ContactName))
            {
                contacts = contacts.Where(c => c.ContactUser.UserName == query.ContactName);
            }

            if (query.IsAccepted)
            {
                contacts = contacts.Where(c => c.IsAccepted);
            }

            if (query.IsBlocked)
            {
                contacts = contacts.Where(c => c.IsBlocked);
            }

            if (query.IsMuted)
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
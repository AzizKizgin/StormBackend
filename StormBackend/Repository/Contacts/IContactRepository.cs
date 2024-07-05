using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Contact;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IContactRepository: IRepositoryBase<Contact>
    {
        Task<Contact> GetContactAsync(string userId, string contactUserId, bool trackChanges);
        Task<List<Contact>> GetContactsAsync(string userId, SearchContactsQuery query, bool trackChanges);
        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);        
    }
}
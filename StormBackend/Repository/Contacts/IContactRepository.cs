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
        Task<Contact> GetContactAsync(int contactId, bool trackChanges);
        Task<Contact> GetContactAsync(string contactUserName, bool trackChanges);
        Task<List<Contact>> GetContactsAsync(GetContactsQuery query, bool trackChanges);
        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Contact;

namespace StormBackend.Services.Contacts
{
    public interface IContactService
    {
        Task CreateContact(string userId, CreateContactDto createContactInfo);
        Task AcceptContact(string userId, int contactId);
        Task BlockContact(string userId, int contactId);
        Task MuteContact(string userId, int contactId);
        Task<List<ContactDto>> GetContacts(string userId, GetContactsQuery query);
        Task<ContactDto> GetContact(string userId, int contactId);
        Task<ContactDto> GetContact(string userId, string contactUserName);
    }
}
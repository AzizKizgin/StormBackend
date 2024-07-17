using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Dtos.Contact;

namespace StormBackend.Services.Contacts
{
    public interface IContactService
    {
        Task CreateContact(string userId, string contactUserId);
        Task DeleteContact(string userId, int contactId);
        Task<List<ContactDto>> GetContacts(string userId);
        Task<ContactDto> GetContact(string userId, string contactUserId);
    }
}
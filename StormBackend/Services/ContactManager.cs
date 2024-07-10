using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.Contact;
using StormBackend.Models;
using StormBackend.Repository.Contacts;
using StormBackend.Services.Contacts;

namespace StormBackend.Services
{
    public class ContactManager: IContactService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ContactManager(IRepositoryManager manager, IMapper mapper, ITokenService tokenService)
        {
            _manager = manager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task CreateContact(string userId, string contactUserId)
        {
            var existingContact = await _manager.Contact.GetContactAsync(userId, contactUserId, false);
            if (existingContact != null)
            {
                throw new Exception("Contact already exists");
            }

            var contact = new Contact
            {
                UserId = userId,
                ContactUserId = contactUserId,
                AddedAt = DateTime.Now,
            };
            _manager.Contact.CreateContact(contact);
            
            await _manager.SaveAsync();
        }

        public async Task<ContactDto> GetContact(string userId, string contactUserId)
        {
            var contact = await _manager.Contact.GetContactAsync(userId, contactUserId, false);
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<List<ContactDto>> GetContacts(string userId, SearchContactsQuery query)
        {
            var contacts = await _manager.Contact.GetContactsAsync(userId, query, false);
            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public async Task DeleteContact(string userId, int contactId)
        {
            var contact = await _manager.Contact.GetContactByIdAsync(contactId, false);
            if (contact == null)
            {
                throw new Exception("Contact not found");
            }
            if (contact.UserId != userId)
            {
                throw new Exception("Unauthorized");
            }
            _manager.Contact.DeleteContact(contact);
            
            await _manager.SaveAsync();
        }
    }
}
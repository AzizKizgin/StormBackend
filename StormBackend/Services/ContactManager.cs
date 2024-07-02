using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.Contact;
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

        public Task AcceptContact(string userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public Task BlockContact(string userId, int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateContact(string userId, CreateContactDto createContactInfo)
        {
            var existingContact = await _manager.Contact.GetContactAsync(createContactInfo.ContactUserName, false);
            if (existingContact != null)
            {
                throw new InvalidOperationException("Contact already exists");
            }
            var contactUser = await _manager.User.GetUserByUsernameAsync(createContactInfo.ContactUserName, false);
            var contact = new Models.Contact {
                AddedAt = DateTime.Now,
                IsAccepted = false,
                ContactUserId = contactUser.Id,
                UserId = userId,
                IsBlocked = false,
                IsMuted = false,
            };
            _manager.Contact.CreateContact(contact);

            var contactUserContact = new Models.Contact {
                AddedAt = DateTime.Now,
                IsAccepted = false,
                ContactUserId = userId,
                UserId = contactUser.Id,
                IsBlocked = false,
                IsMuted = false,
            };
            _manager.Contact.CreateContact(contactUserContact);
            await _manager.SaveAsync();
        }

        public async Task<ContactDto> GetContact(string userId, int contactId)
        {
            var contact = await _manager.Contact.GetContactAsync(contactId, false);
            if (contact.UserId != userId && contact.ContactUserId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> GetContact(string userId, string contactUserName)
        {
            var contact = await _manager.Contact.GetContactAsync(contactUserName, false);
            if (contact.UserId != userId && contact.ContactUserId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            return _mapper.Map<ContactDto>(contact);
        }

        public Task<List<ContactDto>> GetContacts(string userId, GetContactsQuery query)
        {
            throw new NotImplementedException();
        }

        public Task MuteContact(string userId, int contactId)
        {
            throw new NotImplementedException();
        }
    }
}
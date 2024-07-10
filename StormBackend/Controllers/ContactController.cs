using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StormBackend.Dtos;
using StormBackend.Dtos.Contact;
using StormBackend.Services.Contacts;

namespace StormBackend.Controllers
{
    public class ContactController: ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize]        
        [HttpPost("api/contact/create")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto createContactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId =User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _contactService.CreateContact(userId, createContactDto.ContactUserId);
                
                var message = new SuccessMessage
                {
                    Message = "Contact created successfully"
                };

                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("api/contact/get-all")]
        public async Task<IActionResult> GetContacts([FromBody] SearchContactsQuery query)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                var contacts = await _contactService.GetContacts(userId, query);
                return Ok(contacts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("api/contact/get")]
        public async Task<IActionResult> GetContact([FromBody] CreateContactDto getContactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                var contact = await _contactService.GetContact(userId, getContactDto.ContactUserId);
                return Ok(contact);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("api/contact/delete")]
        public async Task<IActionResult> DeleteContact([FromBody] DecideContactDto deleteContactDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return BadRequest("User not found");
                }
                await _contactService.DeleteContact(userId, deleteContactDto.Id);
                var message = new SuccessMessage
                {
                    Message = "Contact deleted successfully"
                };
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
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
    [Route("api/contact")]
    [ApiController]
    public class ContactController: ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Authorize]        
        [HttpPost("create")]
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
        [HttpGet("get-all")]
        public async Task<IActionResult> GetContacts()
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
                var contacts = await _contactService.GetContacts(userId);
                contacts.ForEach(contact =>
                {
                    contact.ContactUser.IsContactOfCurrentUser = true;
                });
                
                return Ok(contacts);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
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
                await _contactService.DeleteContact(userId, id);
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

        [Authorize]
        [HttpDelete("removeByUserId/{id}")]
        public async Task<IActionResult> DeleteContactByUserId([FromRoute] string id)
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
                await _contactService.DeleteContact(userId, id);
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
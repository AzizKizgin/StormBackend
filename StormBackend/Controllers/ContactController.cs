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
                await _contactService.CreateContact(userId, createContactDto);
                
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
    }
}
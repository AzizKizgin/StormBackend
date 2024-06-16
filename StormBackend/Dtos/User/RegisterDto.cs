using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record RegisterDto
    {
        [Required (ErrorMessage = "Email is required")]
        [EmailAddress (ErrorMessage = "Email is not valid")]
        public string Email { get; init; }

        [Required (ErrorMessage = "Name is required")]
        [MinLength(1, ErrorMessage = "Name must not be empty")]
        [MaxLength(20, ErrorMessage = "Name must not be longer than 20 characters")]
        public string Name { get; init; }

        [Required (ErrorMessage = "Password is required")]
        public string Password { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}
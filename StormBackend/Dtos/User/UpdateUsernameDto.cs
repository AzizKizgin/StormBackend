using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record UpdateUsernameDto
    {
        [Required (ErrorMessage = "Username is required")]
        [MinLength(1, ErrorMessage = "Username must not be empty")]
        [MaxLength(20, ErrorMessage = "Username must not be longer than 20 characters")]
        public string Username { get; init; }
    }
}
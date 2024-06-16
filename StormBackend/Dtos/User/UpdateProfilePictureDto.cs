using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Dtos.User
{
    public record UpdateProfilePictureDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Profile picture must not be empty")]
       public string ProfilePicture { get; init; }
    }
}
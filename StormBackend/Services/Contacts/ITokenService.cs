using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Services.Contacts
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
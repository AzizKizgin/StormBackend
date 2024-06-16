using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StormBackend.Dtos.User;
using StormBackend.Models;

namespace StormBackend.Utilities
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<UpdateProfilePictureDto, User>();
            CreateMap<UpdateUserAboutDto, User>();
            CreateMap<UpdateUsernameDto, User>();
        }
    }
}
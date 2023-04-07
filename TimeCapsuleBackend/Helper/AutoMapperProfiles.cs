using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCapsuleBackend.Data.DTOs;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO,User>();

        }
    }
}

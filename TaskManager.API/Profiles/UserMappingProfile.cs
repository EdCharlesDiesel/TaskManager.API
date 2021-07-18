using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Authentication;
using TaskManager.API.Models;

namespace TaskManager.API.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserMaster, UserMaster>();
            CreateMap<RegisterModel, UserMaster>();
            CreateMap<UpdateModel, UserMaster>();
        }
    }
}

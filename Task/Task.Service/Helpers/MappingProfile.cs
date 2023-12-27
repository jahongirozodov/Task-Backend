using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Domain.Entites;
using Task.Service.DTOs.Users;

namespace Task.Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<User,UserRegisterDto>().ReverseMap();
            CreateMap<User,UserResultDto>().ReverseMap();
        }
    }
}

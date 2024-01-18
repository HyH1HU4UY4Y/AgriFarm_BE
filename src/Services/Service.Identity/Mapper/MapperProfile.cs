﻿using AutoMapper;
using Service.Identity.Commands;
using Service.Identity.DTOs;
using SharedDomain.Entities.Users;

namespace Service.Identity.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Member, UserResponse>().ReverseMap();
            CreateMap<Member, CreateMemberCommand>().ReverseMap();
        }
    }
}

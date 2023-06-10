using AutoMapper;
using JobPortal.API.Models;
using JobPortal.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.API.Helpers.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>(); //<Source, Destination>
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
        
    }
}

using AutoMapper;
using EventBus.Messages.Events;
using UserManagment.Dto;
using UserManagment.Dto.ServicesDto;
using UserManagment.Entities;

namespace UserManagment.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto,AppUser>().ReverseMap();
            CreateMap<UserCheckInEventDto,UserCheckInEvent>();
            CreateMap<UserCheckOutEventDto,UserCheckOutEvent>();
        }
    }
}
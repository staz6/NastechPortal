using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
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
            CreateMap<Employee,UsersInfoDto>().ReverseMap();       
            // CreateMap<UserCheckInEventDto,UserCheckInEvent>();
            // CreateMap<UserCheckOutEventDto,UserCheckOutEvent>();
            // CreateMap<UserGetAttendanceEventRequestDto,UserGetAttendanceEventRequest>();
            // CreateMap<UserLeaveEventDto,UserLeaveEvent>();
            // CreateMap<GetAttendanceRecordEventDto,SendAttendanceRecordEventDto>().ForMember(
            //     m => m.EmployeeId, o => o.MapFrom(s => s.BiometricId)
            // );
            
            
        }
    }
}
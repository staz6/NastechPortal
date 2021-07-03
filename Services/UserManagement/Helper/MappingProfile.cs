using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using UserManagement.Dto;
using UserManagement.Dto.ServicesDto;
using UserManagement.Entities;

namespace UserManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto,AppUser>().ReverseMap();
            CreateMap<Employee,UsersInfoDto>()
                .ForMember(x => x.Email, o => o.MapFrom(m => m.AppUser.Email))  
                .ForMember(x => x.Name, o => o.MapFrom(m => m.AppUser.Name))
                .ForMember(x => x.Address, o => o.MapFrom(m => m.AppUser.Address))
                .ForMember(x => x.MobileNumber, o => o.MapFrom(m => m.AppUser.ContactNumber))
                .ForMember(x => x.PersonalEmail, o => o.MapFrom(m => m.AppUser.PersonalEmail))
                .ForMember(x => x.Id, o => o.MapFrom(m => m.AppUser.Id));
            // CreateMap<UserCheckInEventDto,UserCheckInEvent>();
            // CreateMap<UserCheckOutEventDto,UserCheckOutEvent>();
            // CreateMap<UserGetAttendanceEventRequestDto,UserGetAttendanceEventRequest>();
            // CreateMap<UserLeaveEventDto,UserLeaveEvent>();
            // CreateMap<GetAttendanceRecordEventDto,SendAttendanceRecordEventDto>().ForMember(
            //     m => m.EmployeeId, o => o.MapFrom(s => s.BiometricId)
            // );

            CreateMap<EditEmployeeInfoDto,AppUser>();
            
            
            
        }
    }
}
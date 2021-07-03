using AttendanceManagement.Dto;
using AttendanceManagement.Entities;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;

namespace AttendanceManagement.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            
            
            CreateMap<Attendance,GetAttendanceDto>().ReverseMap();
            CreateMap<UserLeaveDto,Leave>().ReverseMap();
            CreateMap<GetUserLeaveDto,Leave>().ReverseMap();
            CreateMap<Attendance,UserGetAttendanceEventDto>();
            CreateMap<Attendance,DeductSalaryEventDto>().ReverseMap();
                
        }
    }
}
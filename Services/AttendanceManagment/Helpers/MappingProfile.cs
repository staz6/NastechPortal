using AttendanceManagment.Dto;
using AttendanceManagment.Entities;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;

namespace AttendanceManagment.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            
            
            CreateMap<Attendance,GetAttendanceDto>().ReverseMap();
            CreateMap<UserLeaveDto,Leave>().ReverseMap();
            CreateMap<GetUserLeaveDto,Leave>().ReverseMap();
        }
    }
}
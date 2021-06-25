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
            CreateMap<Attendance,UserCheckInEvent>().ReverseMap();
            CreateMap<Attendance,UserCheckOutEvent>().ReverseMap();
            CreateMap<Attendance,UserGetAttendanceEventResponse>().ReverseMap();
            CreateMap<Attendance,AttendanceEventDto>().ReverseMap();
            CreateMap<Leave,UserLeaveEvent>().ReverseMap();
        }
    }
}
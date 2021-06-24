using AttendanceManagment.Entities;
using AutoMapper;
using EventBus.Messages.Events;

namespace AttendanceManagment.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attendance,UserCheckInEvent>().ReverseMap();
            CreateMap<Attendance,UserCheckOutEvent>().ReverseMap();
        }
    }
}
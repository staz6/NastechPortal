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
            
            
            
            CreateMap<GetAttendanceDto,Attendance>().ReverseMap()
            .ForMember(x => x.CheckIn, o=> o.MapFrom(m => (m.CheckIn.Year==0001)
             ? "null" : m.CheckIn.ToString("hh:mm:ss tt") ))
            .ForMember(x => x.CheckOut, o=> o.MapFrom(m => (m.CheckOut.Year==0001)
             ? "null" : m.CheckOut.ToString("hh:mm:ss tt") ))
            .ForMember(x => x.Date,o => o.MapFrom(m => m.Date.ToString("MM/dd/yyyy")));
            CreateMap<UserLeaveDto,Leave>().ReverseMap();
            CreateMap<GetUserLeaveDto,Leave>().ReverseMap();
            CreateMap<Attendance,UserGetAttendanceEventDto>();
            CreateMap<Attendance,DeductSalaryEventDto>().ReverseMap();
                
        }
    }
}
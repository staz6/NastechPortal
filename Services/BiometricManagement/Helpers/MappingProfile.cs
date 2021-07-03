using AutoMapper;
using BiometricManagement.Dto;
using EventBus.Messages.Models;

namespace BiometricManagement.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<GetAttendanceRecordEventDto,FileHelperAttendanceLogDto>();
            CreateMap<GetAttendanceRecordEventDto,FileHelperAttendanceLogDto>().ReverseMap();
                    
        }
    }
}
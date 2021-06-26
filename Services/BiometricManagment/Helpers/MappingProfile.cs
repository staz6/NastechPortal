using AutoMapper;
using BiometricManagment.Dto;
using EventBus.Messages.Models;

namespace BiometricManagment.Helpers
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
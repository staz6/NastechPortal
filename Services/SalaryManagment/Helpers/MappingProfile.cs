using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using SalaryManagment.Dto.ServicesDto;
using SalaryManagment.Entities;

namespace SalaryManagment.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenerateSalaryEvent,Salary>()
                .ForMember(x => x.Amount, o => o.MapFrom(m => m.Salary));
            CreateMap<DeductSalaryEventDto,DeductSalaryConsumerDto>();
        }
    }
}
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using SalaryManagement.Dto;
using SalaryManagement.Dto.ServicesDto;
using SalaryManagement.Entities;

namespace SalaryManagement.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenerateSalaryEvent,Salary>()
                .ForMember(x => x.Amount, o => o.MapFrom(m => m.Salary));
            CreateMap<DeductSalaryEventDto,DeductSalaryConsumerDto>();
            CreateMap<SalaryByMonth,GetSalaryHistoryDto>()
                .ForMember(x => x.Month, o => o.MapFrom( m => m.Month.ToString("yyyy MMMM")))
                .ForMember(x => x.Status, o => o.MapFrom( m => (m.Status==false) ? "Pending": "Paid"));
            CreateMap<SalaryDeduction,GetEmployeeSalaryDeduction>()
                .ForMember(x => x.Date , o => o.MapFrom(m => m .Date.Date));
            CreateMap<PostSalaryDeduction,SalaryDeduction>();
        }
    }
}
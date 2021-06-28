using AutoMapper;
using EventBus.Messages.Events;
using SalaryManagment.Entities;

namespace SalaryManagment.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenerateSalaryEvent,Salary>()
                .ForMember(x => x.EmployeeSalary, o => o.MapFrom(m => m.Salary));
        }
    }
}
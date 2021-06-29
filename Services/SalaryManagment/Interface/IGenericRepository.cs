using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using SalaryManagment.Dto.ServicesDto;

namespace SalaryManagment.Interface
{
    public interface IGenericRepository
    {
        Task generateSalary(GenerateSalaryEvent model);
        Task getMonthlySalary(string userId);
        Task DeductSalary(List<DeductSalaryConsumerDto> model);
    }
}
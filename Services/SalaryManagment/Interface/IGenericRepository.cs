using System.Threading.Tasks;
using EventBus.Messages.Events;

namespace SalaryManagment.Interface
{
    public interface IGenericRepository
    {
        Task generateSalary(GenerateSalaryEvent model);
    }
}
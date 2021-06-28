using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
using SalaryManagment.Interface;

namespace SalaryManagment.EventBusConsumer
{
    public class GenerateSalaryEventConsumer : IConsumer<GenerateSalaryEvent>
    {
        private readonly IGenericRepository _repo;
        public GenerateSalaryEventConsumer(IGenericRepository repo)
        {
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<GenerateSalaryEvent> context)
        {
           await _repo.generateSalary(context.Message);
        }
    }
}
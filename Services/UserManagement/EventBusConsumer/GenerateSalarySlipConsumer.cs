using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagement.Interface;

namespace UserManagement.EventBusConsumer
{
    public class GenerateSalarySlipConsumer : IConsumer<GenerateSalarySlipRequest>
    {
        /// <summary>
        /// To get user info when generating salary slip
        /// </summary>
        private readonly IAccountRepository _repo;
        private readonly ILogger<GenerateSalarySlipConsumer> _logger;
        public GenerateSalarySlipConsumer(IAccountRepository repo, ILogger<GenerateSalarySlipConsumer> logger)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<GenerateSalarySlipRequest> context)
        {
            _logger.LogInformation("user generate salary slip");
            var user = await _repo.getCurrentUser(context.Message.UserId);
            GenerateSalarySlipResponse response = new GenerateSalarySlipResponse
            {
                Name = user.Name,
                Address = user.Address,
                CurrentSalary = user.CurrentSalary,
                Designation = user.Designation,
                EmployeeId = user.EmployeeId,
                JoiningDate = user.JoiningDate.ToString()
            };
            await context.RespondAsync(response);
        }
    }
}
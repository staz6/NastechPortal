using System.Threading.Tasks;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AttendanceManagment.EventBusConsumer
{
    public class UserCheckInConsumer : IConsumer<UserCheckInEvent>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;
        private readonly ILogger<UserCheckInConsumer> _logger;
        public UserCheckInConsumer(IMapper mapper, IGenericRepository repo, 
        ILogger<UserCheckInConsumer> logger)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserCheckInEvent> context)
        {
            var command = _mapper.Map<Attendance>(context.Message);
            await _repo.CheckIn(command);
            _logger.LogInformation("Queeu Success");
        }
    }
}
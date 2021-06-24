using System.Threading.Tasks;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AttendanceManagment.EventBusConsumer
{
    public class UserCheckOutConsumer : IConsumer<UserCheckOutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;
        private readonly ILogger<UserCheckInConsumer> _logger;
        public UserCheckOutConsumer(IMapper mapper, IGenericRepository repo,
        ILogger<UserCheckInConsumer> logger)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UserCheckOutEvent> context)
        {
            var command = _mapper.Map<Attendance>(context.Message);
            await _repo.CheckOut(command);
            
            
        }
    }
}
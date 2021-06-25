using System.Threading.Tasks;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;

namespace AttendanceManagment.EventBusConsumer
{
    public class UserLeaveConsumer : IConsumer<UserLeaveEvent>
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;
        public UserLeaveConsumer(IGenericRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<UserLeaveEvent> context)
        {
            var message = _mapper.Map<Leave>(context.Message);
            await _repo.leaveRequest(message);
        }
    }
}
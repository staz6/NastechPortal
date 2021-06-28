using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagment.Interface;

namespace UserManagment.EventBusConsumer
{
    public class AttendanceRecordConsumer : IConsumer<GetAttendanceRecordEvent>
    {
        private readonly ILogger<AttendanceRecordConsumer> _logger;
        private readonly IAccountRepository _repo;
        public AttendanceRecordConsumer(ILogger<AttendanceRecordConsumer> logger, IAccountRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetAttendanceRecordEvent> context)
        {
            _logger.LogInformation(context.Message.GetAttendanceRecord.ToString());
            await _repo.GetAttendanceRecord(context.Message.GetAttendanceRecord);
        }
    }
}
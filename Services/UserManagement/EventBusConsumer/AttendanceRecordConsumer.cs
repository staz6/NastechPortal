using System.Threading.Tasks;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagement.Interface;

namespace UserManagement.EventBusConsumer
{
    public class AttendanceRecordConsumer : IConsumer<GetAttendanceRecordEvent>
    {
        /// <summary>
        /// Biometric micro service will call this consumer every time attendance record is generated
        /// </summary>
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
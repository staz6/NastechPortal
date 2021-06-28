using System.Collections.Generic;
using EventBus.Messages.Models;

namespace EventBus.Messages.Events
{
    public class SendAttendanceRecordEvent : IntegrationBaseEvent
    {
        public List<SendAttendanceRecordEventDto> SendAttendanceRecord {get;set;} 
    }
}
using System.Collections.Generic;
using EventBus.Messages.Models;

namespace EventBus.Messages.Events
{
    public class GetAttendanceRecordEvent : IntegrationBaseEvent
    {
        public IEnumerable<GetAttendanceRecordEventDto> GetAttendanceRecord{get;set;}
    }
}
using System;

namespace EventBus.Messages.Models
{
    public class GetAttendanceRecordEventDto
    {
        public int BiometricId { get; set; }
        public DateTime TimeStamp {get;set;}
    }
}
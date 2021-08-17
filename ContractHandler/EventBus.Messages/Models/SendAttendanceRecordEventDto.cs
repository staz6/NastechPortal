using System;

namespace EventBus.Messages.Models
{
    public class SendAttendanceRecordEventDto
    {
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
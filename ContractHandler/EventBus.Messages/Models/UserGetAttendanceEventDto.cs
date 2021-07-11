using System;

namespace EventBus.Messages.Models
{
    public class UserGetAttendanceEventDto
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string ShiftTiming { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
        public string TimeStatus {get;set;}
    }
}
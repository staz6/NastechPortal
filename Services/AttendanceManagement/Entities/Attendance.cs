using System;

namespace AttendanceManagement.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ShiftTiming { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime Date { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
    }
}
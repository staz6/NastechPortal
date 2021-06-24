using System;

namespace AttendanceManagment.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ShiftTiming { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Date { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
    }
}
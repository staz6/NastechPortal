using System;

namespace AttendanceManagment.Dto
{
    public class GetAttendanceDto
    {
        public int Id { get; set; }
        public string ShiftTiming { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Date { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
    }
}
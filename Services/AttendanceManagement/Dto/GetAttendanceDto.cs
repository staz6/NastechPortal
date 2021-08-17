using System;

namespace AttendanceManagement.Dto
{
    public class GetAttendanceDto
    {
        
        public string ShiftTiming { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Date { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
    }
}
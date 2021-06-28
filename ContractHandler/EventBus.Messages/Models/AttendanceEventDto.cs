namespace EventBus.Messages.Models
{
    public class AttendanceEventDto
    {
        public int UserId { get; set; }
        public string Date { get; set; }
        public string ShiftTiming { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string WorkedHours { get; set; }
        public string EffectiveHours { get; set; }
        public string Status { get; set; }
    }
}
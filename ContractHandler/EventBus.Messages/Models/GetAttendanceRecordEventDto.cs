namespace EventBus.Messages.Models
{
    public class GetAttendanceRecordEventDto
    {
        public int BiometricId { get; set; }
        public string TimeStamp {get;set;}
    }
}
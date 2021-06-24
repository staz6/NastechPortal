namespace EventBus.Messages.Events
{
    public class UserCheckInEvent : IntegrationBaseEvent
    {
        public string UserId{get;set;}
        public string  ShiftTiming { get; set; }
        
    }
}
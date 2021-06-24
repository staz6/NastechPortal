namespace EventBus.Messages.Events
{
    public class UserCheckOutEvent : IntegrationBaseEvent
    {
        public string UserId { get; set; }
    }
}
namespace EventBus.Messages.Events
{
    public class GetUserByIdEventRequest : IntegrationBaseEvent
    {
        public string Id { get; set; }
    }
}
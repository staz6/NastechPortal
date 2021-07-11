namespace EventBus.Messages.Events
{
    public class GenerateSalaryEvent : IntegrationBaseEvent
    {
        public string UserId { get; set; }
        public int Salary { get; set; }
    }
}
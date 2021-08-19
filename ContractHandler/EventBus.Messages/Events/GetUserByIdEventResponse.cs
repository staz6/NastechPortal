namespace EventBus.Messages.Events
{
    public class GetUserByIdEventResponse : IntegrationBaseEvent
    {
        public string AppUserId { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}
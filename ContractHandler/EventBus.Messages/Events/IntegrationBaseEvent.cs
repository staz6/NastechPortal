using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            EventId = Guid.NewGuid();
            CreationData = DateTime.Now;
        }

        public IntegrationBaseEvent(Guid eventId, DateTime creationData)
        {
            EventId = eventId;
            CreationData = creationData;
        }

        public Guid EventId { get; private set; }
        public DateTime CreationData{ get; private set; }
    }
}
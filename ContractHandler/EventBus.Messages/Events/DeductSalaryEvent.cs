using System.Collections.Generic;
using EventBus.Messages.Models;

namespace EventBus.Messages.Events
{
    public class DeductSalaryEvent : IntegrationBaseEvent
    {
        public List<DeductSalaryEventDto> deductSalaryEvent { get; set; }
    }
}
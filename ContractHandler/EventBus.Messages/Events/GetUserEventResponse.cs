using System.Collections.Generic;
using EventBus.Messages.Models;

namespace EventBus.Messages.Events
{
    public class GetUserEventResponse
    {
        public List<GetUserResponseDto> getUserResponse {get;set;}
    }
}
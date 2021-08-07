using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagement.Helper;
using UserManagement.Interface;

namespace UserManagement.EventBusConsumer
{
    /// <summary>
    /// This is the Class which every service will call when they need employee info 
    /// </summary>
    public class GetUserEventConsumer : IConsumer<GetUserEventRequest>
    {
        private readonly ILogger<GetUserEventConsumer> _logger;
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;
        public GetUserEventConsumer(ILogger<GetUserEventConsumer> logger, IAccountRepository repo
            ,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _logger = logger;
        }

    public async Task Consume(ConsumeContext<GetUserEventRequest> context)
    {
        var obj = await _repo.GetAllUser();
        List<GetUserResponseDto> listObj = new List<GetUserResponseDto>();
        foreach(var item in obj)
        {
                //If u want to return some other fields add the new field in GetUserResponseDto nad map it here 
                var a = new GetUserResponseDto{
                Name=item.AppUser.Name,
                UserId=item.AppUserId,
                ShiftTiming=item.ShiftTiming              
                };
                listObj.Add(a);
            
     
            
        }
       GetUserEventResponse response = new GetUserEventResponse{
           getUserResponse=listObj
       };
       await context.RespondAsync(response);

    }
}
}
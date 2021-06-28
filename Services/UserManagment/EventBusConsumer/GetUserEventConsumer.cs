using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagment.Interface;

namespace UserManagment.EventBusConsumer
{
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
            var a = new GetUserResponseDto{
                Name=item.AppUser.Name,
                UserId=item.AppUserId
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
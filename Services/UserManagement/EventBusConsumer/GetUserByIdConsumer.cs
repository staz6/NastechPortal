using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserManagement.Dto;
using UserManagement.Interface;

namespace UserManagement.EventBusConsumer
{
    public class GetUserByIdConsumer : IConsumer<GetUserByIdEventRequest>
    {
        private readonly ILogger<GetUserByIdConsumer> _logger;
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;
        public GetUserByIdConsumer(ILogger<GetUserByIdConsumer> logger, IAccountRepository repo
            ,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<GetUserByIdEventRequest> context)
        {
            try{
                var obj =await _repo.getCurrentUser(context.Message.Id);
            var response = _mapper.Map<UsersInfoDto,GetUserByIdEventResponse>(obj);
            await context.RespondAsync(response);
            }
            catch{
                throw new System.Exception("Not Found");
            }
            
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using SalaryManagment.Dto.ServicesDto;
using SalaryManagment.Interface;

namespace SalaryManagment.EventBusConsumer
{
    public class DeductSalaryEventConsumer : IConsumer<DeductSalaryEvent>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _repo;
        public DeductSalaryEventConsumer(IMapper mapper, IGenericRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<DeductSalaryEvent> context)
        {
            var mapObject = _mapper.Map<List<DeductSalaryConsumerDto>>(context.Message.deductSalaryEvent);
            await _repo.DeductSalary(mapObject);
        }
    }
}
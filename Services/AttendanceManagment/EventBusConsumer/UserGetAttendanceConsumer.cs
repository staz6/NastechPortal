using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;

namespace AttendanceManagment.EventBusConsumer
{
    public class UserGetAttendanceConsumer : IConsumer<UserGetAttendanceEventRequest>
    {
        private readonly IGenericRepository _repo;
        private readonly IMapper _mapper;
        public UserGetAttendanceConsumer(IGenericRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task Consume(ConsumeContext<UserGetAttendanceEventRequest> context)
        {
            var attandaceList = await _repo.getUserAttendance(context.Message.UserId);
            var mapResult = _mapper.Map<IEnumerable<Attendance>,IEnumerable<AttendanceEventDto>>(attandaceList);
            UserGetAttendanceEventResponse result = new UserGetAttendanceEventResponse{
                Attendance=mapResult
            };
            await context.RespondAsync(result);
        }
    }
}
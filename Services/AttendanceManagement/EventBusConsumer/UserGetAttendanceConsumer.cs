using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceManagement.Data;
using AttendanceManagement.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;

namespace AttendanceManagement.EventBusConsumer
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
            var userId = context.Message.UserId;
            DateTime month = context.Message.Month;
            var userAttendance = await _repo.getUserAttendanceByMonth(userId,month);
            var mapObject = _mapper.Map<List<UserGetAttendanceEventDto>>(userAttendance);
            UserGetAttendanceEventResponse response = new UserGetAttendanceEventResponse{
                Attendance=mapObject
            };
            await context.RespondAsync(response);

        }
    }
}
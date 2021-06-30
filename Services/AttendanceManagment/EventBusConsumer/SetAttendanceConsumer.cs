using System.Threading.Tasks;
using MassTransit;
using EventBus.Messages.Events;
using Microsoft.Extensions.Logging;
using AttendanceManagment.Data;
using AttendanceManagment.Entities;
using System.Linq;
using System;
using AutoMapper;
using EventBus.Messages.Models;
using System.Collections.Generic;

namespace AttendanceManagment.EventBusConsumer
{
    public class SetAttendanceConsumer : IConsumer<SendAttendanceRecordEvent>
    {
        private readonly ILogger<SetAttendanceConsumer> _logger;
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public SetAttendanceConsumer(ILogger<SetAttendanceConsumer> logger, IMapper mapper
        , AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendAttendanceRecordEvent> context)
        {
            _logger.LogInformation("setattendance");

            foreach (var items in context.Message.SendAttendanceRecord)
            {
                var chkOut = _context.Attendances
                        .FirstOrDefault(x => x.UserId == items.UserId && x.Date == items.TimeStamp.Date);
                if (chkOut != null)
                {
                    if (chkOut.CheckOut != null)
                    {
                        chkOut.CheckOut = items.TimeStamp;
                        chkOut.WorkedHours = (chkOut.CheckOut - chkOut.CheckIn).ToString();
                        await _context.SaveChangesAsync();
                    }

                }
                else
                {
                    TimeSpan start = TimeSpan.Parse("10:00:00");
                    TimeSpan end = TimeSpan.Parse("10:15:00");
                    Attendance obj = new Attendance
                    {
                        CheckIn = items.TimeStamp,
                        ShiftTiming = items.ShiftTiming,
                        UserId = items.UserId,
                        Date = items.TimeStamp.Date,
                        Status = "Present"

                    };
                    if (obj.CheckIn.Hour < 10)
                    {
                        obj.EffectiveHours = obj.CheckIn.ToString("hh-mm") + " early";
                        obj.Status = "Early";
                    }
                    else if (obj.CheckIn.Hour == 10 && obj.CheckIn.Minute < 15)
                    {
                        obj.EffectiveHours = obj.CheckIn.ToString("hh-mm") + " grace";
                        obj.Status = "Grace";
                    }
                    else
                    {
                        obj.EffectiveHours = obj.CheckIn.ToString("hh-mm") + " late";
                        obj.Status = "Late";
                    }
                    await _context.Attendances.AddAsync(obj);
                    await _context.SaveChangesAsync();
                }
            }
            var absentCheck = _context.Attendances.ToList();

            foreach (var items in absentCheck)
            {
                if (items.CheckIn == null)
                {
                    items.Status = "Absent";
                }
            }

            // var deductSalary = _context.Attendances.Where(x => x.Status == "Late" || x.Status == "Absent").ToList();
            
            // var deductSalaryMapObject = _mapper.Map<List<DeductSalaryEventDto>>(deductSalary);
            // _logger.LogInformation(deductSalaryMapObject.ToString());
            // DeductSalaryEvent deductSalaryEvent = new DeductSalaryEvent{
            //     deductSalaryEvent=deductSalaryMapObject
            // };
            // _logger.LogInformation("Deduct Salary event");
            // await _publishEndpoint.Publish(deductSalaryEvent);

            
            
        }
    }
}
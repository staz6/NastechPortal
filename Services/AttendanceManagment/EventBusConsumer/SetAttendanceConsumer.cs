using System.Threading.Tasks;
using MassTransit;
using EventBus.Messages.Events;
using Microsoft.Extensions.Logging;
using AttendanceManagment.Data;
using AttendanceManagment.Entities;
using System.Linq;
using System;

namespace AttendanceManagment.EventBusConsumer
{
    public class SetAttendanceConsumer : IConsumer<SendAttendanceRecordEvent>
    {
        private readonly ILogger<SetAttendanceConsumer> _logger;
        private readonly AppDbContext _context;
        public SetAttendanceConsumer(ILogger<SetAttendanceConsumer> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SendAttendanceRecordEvent> context)
        {
           _logger.LogInformation("setattendance");

             foreach(var items in context.Message.SendAttendanceRecord)
            {
                var chkOut = _context.Attendances
                        .FirstOrDefault(x => x.UserId == items.UserId && x.Date == items.TimeStamp.ToString("dd-MM-yyyy"));
                if(chkOut != null)
                {
                    if(chkOut.CheckOut != null)
                    {
                        chkOut.CheckOut=items.TimeStamp;
                        chkOut.WorkedHours = (chkOut.CheckOut-chkOut.CheckIn).ToString();
                        await _context.SaveChangesAsync();
                    }
                    
                }
                else{
                    TimeSpan start = TimeSpan.Parse("10:00:00");
                    TimeSpan end = TimeSpan.Parse("10:15:00");
                    Attendance obj = new Attendance{    
                    CheckIn = items.TimeStamp,
                    ShiftTiming= items.ShiftTiming,
                    UserId=items.UserId,
                    Date= items.TimeStamp.ToString("dd-MM-yyyy")
                    };
                    if(obj.CheckIn.Hour<10)
                    {
                        obj.Status ="Early";
                    }
                    if(obj.CheckIn.Hour ==10 && obj.CheckIn.Minute <15) 
                    {
                        obj.Status ="Grace";
                    }    
                    else{
                        obj.Status="Late";
                    }
                    await _context.Attendances.AddAsync(obj);
                    await _context.SaveChangesAsync();
                }                   
            }
            var absentCheck = _context.Attendances.ToList();
            
            foreach(var items in absentCheck)
            {
                if(items.CheckIn==null)
                {
                    items.Status="Absent";            
                }
            }
        }
    }
}
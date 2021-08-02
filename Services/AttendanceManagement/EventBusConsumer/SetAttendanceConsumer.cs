using System.Threading.Tasks;
using MassTransit;
using EventBus.Messages.Events;
using Microsoft.Extensions.Logging;
using AttendanceManagement.Data;
using AttendanceManagement.Entities;
using System.Linq;
using System;
using AutoMapper;
using EventBus.Messages.Models;
using System.Collections.Generic;

namespace AttendanceManagement.EventBusConsumer
{
    /// <summary>
    /// This is the consumer class which biometric call for daily attendance record 
    /// REMEBER TO FIX ADD LEAVE FUNCTIONALITY 
    /// </summary>
    public class SetAttendanceConsumer : IConsumer<SendAttendanceRecordEvent>
    {
        private readonly ILogger<SetAttendanceConsumer> _logger;
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetUserEventRequest> _requestClient;
        public SetAttendanceConsumer(ILogger<SetAttendanceConsumer> logger, IMapper mapper     
        , AppDbContext context, IPublishEndpoint publishEndpoint,IRequestClient<GetUserEventRequest> requestClient)
        {
            _requestClient = requestClient;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _context = context;
            _logger = logger;
        }

    public async Task Consume(ConsumeContext<SendAttendanceRecordEvent> context)
    {
        _logger.LogInformation("setattendance");
        
        /// context.message.sendattendancerecord container the biometric attendance records
        foreach (var items in context.Message.SendAttendanceRecord)
        {
            /// If no record exist for that date then create a default records for all employee marking
            /// them absents
            var chkInitRecord = _context.Attendances.FirstOrDefault(x => x.Date == items.TimeStamp.Date);
            if (chkInitRecord == null)
            {
                var request = _requestClient.Create( new GetUserEventRequest{});
                var response = await request.GetResponse<GetUserEventResponse>();
                foreach(var item in response.Message.getUserResponse)
                {
                    
                    Attendance attendanceObj = new Attendance{
                        UserId= item.UserId,
                        ShiftTiming=item.ShiftTiming,
                        Status="Absent",
                        Date=items.TimeStamp.Date   
                    };
                    await _context.Attendances.AddAsync(attendanceObj);
                    await _context.SaveChangesAsync();
                }
            } 
            
            // This is the checkout check if record exist for date then put the timestamp in checkout
            // else this is a check in request
            var chkOut = _context.Attendances
                    .FirstOrDefault(x => x.UserId == items.UserId && x.Date.Date == items.TimeStamp.Date);
            
            /// Datetime.Minvalue is because deafult datetime is something 00:00:00 and its problematic to
            /// define nullable datetime property 
            if (chkOut.CheckIn != DateTime.MinValue)
            {
                // if (chkOut.CheckOut != null)
                // {
                    chkOut.CheckOut = items.TimeStamp;
                    chkOut.WorkedHours = (chkOut.CheckOut - chkOut.CheckIn).ToString();
                    await _context.SaveChangesAsync();
                // }
            }
            else
            {
                // TimeSpan start = TimeSpan.Parse("10:00:00");
                // TimeSpan end = TimeSpan.Parse("10:15:00");
                
                    chkOut.CheckIn = items.TimeStamp;
                    chkOut.Status = "Present";

                if (chkOut.CheckIn.Hour < 10)
                {
                    chkOut.EffectiveHours = chkOut.CheckIn.ToString("hh-mm") + " early";
                    
                }
                else if (chkOut.CheckIn.Hour == 10 && chkOut.CheckIn.Minute < 15)
                {
                    chkOut.EffectiveHours = chkOut.CheckIn.ToString("hh-mm") + " grace";
                    
                }
                else
                {
                    chkOut.Status= "Late";
                    chkOut.EffectiveHours = chkOut.CheckIn.ToString("hh-mm") + " late";
                    
                }
                
                await _context.SaveChangesAsync();
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
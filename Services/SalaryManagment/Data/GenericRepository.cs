using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalaryManagment.Dto.ServicesDto;
using SalaryManagment.Entities;
using SalaryManagment.Interface;

namespace SalaryManagment.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRequestClient<UserGetAttendanceEventRequest> _requestClient;
        private readonly ILogger<GenericRepository> _logger;
        public GenericRepository(AppDbContext context, IMapper mapper,
        IRequestClient<UserGetAttendanceEventRequest> requestClient, ILogger<GenericRepository> logger)
        {
            _logger = logger;
            _requestClient = requestClient;
            _mapper = mapper;
            _context = context;
        }

        public async Task DeductSalary(List<DeductSalaryConsumerDto> model)
        {
             var groupByid = model.GroupBy(x => x.UserId);
             foreach(var items in groupByid)
             {
                int leaveCount = 0;
                foreach(var item in items)
                {
                    var usr = _context.Salarys.Include(x => x.SalaryBreakdown).FirstOrDefault(x => x.UserId==item.UserId);
                    int deductAmount = usr.SalaryBreakdown.DaySalary/2;
                    if(item.Status=="Absent")
                    {
                        var chk = await _context.SalaryDeductions.FirstOrDefaultAsync(x => x.UserId==item.UserId && x.Date == item.Date);
                        if(chk==null)
                        {
                            SalaryDeduction obj = new SalaryDeduction{
                            UserId=item.UserId,
                            Date=item.Date,
                            Reason="Absent you was",
                            Amount=deductAmount
                        };
                            await _context.SalaryDeductions.AddAsync(obj);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else{
                        leaveCount++;
                        if(leaveCount==4)
                        {
                            var chk = await _context.SalaryDeductions.FirstOrDefaultAsync(x => x.UserId==item.UserId && x.Date == item.Date);
                            if(chk==null)
                            {
                                 SalaryDeduction obj = new SalaryDeduction{
                                UserId=item.UserId,
                                Date=item.Date,
                                Reason="Late you was",
                                Amount=deductAmount
                                };
                                await _context.SalaryDeductions.AddAsync(obj);
                                await _context.SaveChangesAsync();
                                
                            }
                            leaveCount=leaveCount-2;
                        }
                    }
                }
             }                            
        }

        public async Task generateSalary(GenerateSalaryEvent model)
    {
        var mapObject = _mapper.Map<Salary>(model);
        mapObject.SalaryBreakdown = new SalaryBreakdown
        {
            DaySalary = mapObject.Amount / 30,
            Date = DateTime.Now, 
        };
        mapObject.Appraisal = new Appraisal {};
        mapObject.SalaryHistory = new SalaryHistory {};
        
        _context.Salarys.AddAsync(mapObject).GetAwaiter().GetResult();
        await _context.SaveChangesAsync();
    }

    public async Task getMonthlySalary(string userId)
    {
        // using var request = _requestClient.Create(new UserGetAttendanceEventRequest { UserId = userId });
        // var response = await request.GetResponse<UserGetAttendanceEventResponse>();
        // List<UserGetAttendanceEventDto> attendanceRecord = response.Message.Attendance;
        // //2021-03-24 18:35:57
        // DateTime dateTime = new DateTime(2021,04,24);
        // var attendanceObj = attendanceRecord.Where(x => x.CheckIn.Year==dateTime.Year &&
        //     x.CheckIn.Month == dateTime.Month);
        
        // // Deducte Late Salary
        // // var lateAttendance = attendanceObj.Where(x => x.TimeStatus=="Late");
        // // int count = lateAttendance.Count();
        // // if(count>3)
        // // {
            
        // // }
        // var salaryObject = await _context.Salarys.Include(x => x.SalaryHistory).FirstOrDefaultAsync(x=>x.UserId == userId);
        // var salaryDeduction = await _context.SalaryDeductions
        //     .FirstOrDefaultAsync(x => x.UserId == salaryObject.UserId && x.Date.Year==dateTime.Year &&
        //     x.Date.Month == dateTime.Month);
        // var salaryBonus = await _context.Bonuss
        //     .FirstOrDefaultAsync(x => x.UserId == salaryObject.UserId && x.Date.Year==dateTime.Year &&
        //     x.Date.Month == dateTime.Month);
        // SalaryByMonth salaryByMonth = new SalaryByMonth{
        //     Amount=salaryObject.Amount,
        //     NetAmount=salaryObject.Amount
        // };
        // salaryObject.SalaryHistory.SalaryByMonth.Add(salaryByMonth);

        // await _context.SaveChangesAsync();
        
        
        return;
    }
}
}
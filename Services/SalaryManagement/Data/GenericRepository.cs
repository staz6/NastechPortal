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
using SalaryManagement.Dto;
using SalaryManagement.Dto.ServicesDto;
using SalaryManagement.Entities;
using SalaryManagement.Interface;

namespace SalaryManagement.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRequestClient<UserGetAttendanceEventRequest> _requestClient;
        private readonly IRequestClient<GetUserEventRequest> _getUserClient;
        private readonly ILogger<GenericRepository> _logger;
        private readonly IRequestClient<GenerateSalarySlipRequest> _getSalaryClient;
        public GenericRepository(AppDbContext context, IMapper mapper,
        IRequestClient<UserGetAttendanceEventRequest> requestClient, ILogger<GenericRepository> logger,
        IRequestClient<GetUserEventRequest> getUserClient, IRequestClient<GenerateSalarySlipRequest> getSalaryClient)
        {
            _getSalaryClient = getSalaryClient;
            _logger = logger;
            _requestClient = requestClient;
            _mapper = mapper;
            _getUserClient = getUserClient;
            _context = context;

        }



    public async Task generateSalary(GenerateSalaryEvent model)
    {
        var mapObject = _mapper.Map<Salary>(model);
        var chk = await _context.Salarys.FirstOrDefaultAsync(x => x.UserId == mapObject.UserId);

        if (chk != null)
        {
            chk.Amount = model.Salary;
        }
        else
        {
            mapObject.SalaryBreakdown = new SalaryBreakdown
            {
                DaySalary = mapObject.Amount / 30,
                Date = DateTime.Now,
            };
            _context.Salarys.AddAsync(mapObject).GetAwaiter().GetResult();
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Below are the four function use for generating Salary or updating it
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>

    public async Task generateMonthlySalary(DateTime date)
    {
        _logger.LogInformation("generateMonthlySalary");
        var request = _getUserClient.Create(new GetUserEventRequest { });
        var response = await request.GetResponse<GetUserEventResponse>();
        foreach (var item in response.Message.getUserResponse)
        {
            _logger.LogInformation("Generating salary Monthly Salary for" + item.UserId);
            await getMonthlySalary(item.UserId,date);
        }
    }
    
    public async Task getMonthlySalary(string userId,DateTime date)
    {
        _logger.LogInformation("getMonthlySalary");
        // DateTime date = DateTime.Now;
        var response = _requestClient.Create(new UserGetAttendanceEventRequest
        {
            UserId = userId,
            Month = date
        });
        var usr = _context.Salarys.Include(x => x.SalaryBreakdown).FirstOrDefault(x => x.UserId == userId);
        int salarayAmount = usr.Amount;
        int deductAmount = ((usr.Amount) / 30) / 2;
        var result = await response.GetResponse<UserGetAttendanceEventResponse>();
        if(result!= null){
            await deductSalary(result.Message.Attendance, deductAmount);
            await monthSalary(userId, date, salarayAmount);
        }
        
        return;
    }


    public async Task deductSalary(List<UserGetAttendanceEventDto> model, int deductAmount)
    {
        _logger.LogInformation("deductSalary");
        //Absent insert
        var absents = model.Where(x => x.Status == "Absent").ToList();
        foreach (var item in absents)
        {
            var chk = await _context.SalaryDeductions.FirstOrDefaultAsync(x => x.UserId == item.UserId && x.Date == item.Date);
            if (chk == null)
            {
                SalaryDeduction obj = new SalaryDeduction
                {
                    UserId = item.UserId,
                    Date = item.Date,
                    Reason = "Absent",
                    Amount = deductAmount * 2,
                    DeductSalary = true
                };
                await _context.SalaryDeductions.AddAsync(obj);
                await _context.SaveChangesAsync();
            }
        }
        var lates = model.Where(x => x.Status == "Late").ToList();
        int iteration = 0;
        foreach (var item in lates)
        {
            iteration++;
            if (iteration == 3)
            {
                var chk = await _context.SalaryDeductions.FirstOrDefaultAsync(x => x.UserId == item.UserId && x.Date == item.Date);
                if (chk == null)
                {
                    SalaryDeduction obj = new SalaryDeduction
                    {
                        UserId = item.UserId,
                        Date = item.Date,
                        Reason = "Late",
                        Amount = deductAmount,
                        DeductSalary = true
                    };
                    await _context.SalaryDeductions.AddAsync(obj);
                    await _context.SaveChangesAsync();
                    iteration = 0;
                }
            }

        }
    }

    public async Task monthSalary(string userId, DateTime month, int salaryAmount)
    {
        _logger.LogInformation("monthSalary");
        SalaryHistory history;
        var salary = await _context.Salarys.FirstOrDefaultAsync(x => x.UserId == userId);
        var salaryHistory = await _context.SalaryHistorys.FirstOrDefaultAsync(x => x.UserId == userId);
        if (salaryHistory == null)
        {
            history = new SalaryHistory
            {
                UserId = userId,
            };
            await _context.SalaryHistorys.AddAsync(history);
            salary.SalaryHistory = history;
            await _context.SaveChangesAsync();

        }
        var salaryMonth = await _context.SalaryByMonths
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Month.Year == month.Year && x.Month.Month == month.Month);
        if (salaryMonth == null)
        {
            int deductionAmount = _context.SalaryDeductions
                    .Where(x => x.UserId == userId && x.Date.Month == month.Month && x.Date.Year == month.Year && x.DeductSalary == true).ToList().Sum(x => x.Amount);
            int bonusAmount = _context.Bonuss
                    .Where(x => x.UserId == userId && x.Date.Month == month.Month && x.Date.Year == month.Year).ToList().Sum(x => x.Amount);
            int netAmount = salaryAmount - deductionAmount + bonusAmount;
            var firstSalaryHistory = await _context.SalaryHistorys.FirstOrDefaultAsync(x => x.UserId == userId);
            SalaryByMonth salaryByMonth = new SalaryByMonth
            {
                UserId = userId,
                Month = month,
                Amount = salaryAmount,
                Deduction = deductionAmount,
                NetAmount = netAmount,
                SalaryHistoryId = firstSalaryHistory.Id,
                Status = false
            };

            await _context.SalaryByMonths.AddAsync(salaryByMonth);
            await _context.SaveChangesAsync();


        }
        else
        {
            var getUserSalaryHistory = await _context.SalaryHistorys.Include(x => x.SalaryByMonth).FirstOrDefaultAsync(x => x.UserId == userId);
            var getSalaryFromHistory = getUserSalaryHistory.SalaryByMonth.FirstOrDefault(x => x.Month.Year == month.Year && x.Month.Month == month.Month);
            int deductionAmount = _context.SalaryDeductions
                    .Where(x => x.UserId == userId && x.Date.Month == month.Month && x.Date.Year == month.Year && x.DeductSalary == true).ToList().Sum(x => x.Amount);
            int bonusAmount = _context.Bonuss
                    .Where(x => x.UserId == userId && x.Date.Month == month.Month && x.Date.Year == month.Year).ToList().Sum(x => x.Amount);
            int netAmount = salaryAmount - deductionAmount + bonusAmount;
            getSalaryFromHistory.Deduction = deductionAmount;
            getSalaryFromHistory.NetAmount = netAmount;

            await _context.SaveChangesAsync();
        }
        return;
    }


    /// <summary>
    /// Monthly Salary
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<SalaryByMonth>> getSalaryHistory(string userId)
    {
        return await _context.SalaryByMonths.Where(x => x.UserId == userId).ToListAsync();
    }


    public async Task<IReadOnlyList<SalaryDeduction>> getEmployeeSalaryHistory(string userId)
    {
        var returnObj = await _context.SalaryDeductions.Where(x => x.UserId == userId).ToListAsync();
        return returnObj;
    }

    public async Task postSalaryDeduction(SalaryDeduction model)
    {
        await _context.SalaryDeductions.AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task<GenerateSalarySlipDto> generateSalarySlip(int id)
    {
        
        
        var salary =await _context.SalaryByMonths.FirstOrDefaultAsync(x => x.Id==id);
            
        var request = _getSalaryClient.Create(new GenerateSalarySlipRequest{ UserId=salary.UserId});
        var response = await request.GetResponse<GenerateSalarySlipResponse>();
        var absent = _context.SalaryDeductions
            .Where(x => x.UserId==salary.UserId && x.Date.Month==salary.Month.Month && x.Date.Year == salary.Month.Year && x.Reason=="Absent").Sum(x => x.Amount);
        var late = _context.SalaryDeductions
            .Where(x => x.UserId==salary.UserId && x.Date.Month==salary.Month.Month && x.Date.Year == salary.Month.Month && x.Reason=="Late").Sum(x => x.Amount);
        if(salary == null)
        {
            throw new Exception();
            
        }
        else
        {
            return new  GenerateSalarySlipDto{
                Name=response.Message.Name,
                Address=response.Message.Address,
                CurrentSalary=response.Message.CurrentSalary,
                Amount=salary.NetAmount,
                TotalDays="31",
                EarningDays="31",
                PerDaySalary=response.Message.CurrentSalary/30,
                Absent=absent,
                Late=late,
                EmployeeId=response.Message.EmployeeId,
                Designation=response.Message.Designation,
                FatherName=response.Message.FatherName,
                JoiningDate=response.Message.JoiningDate

            };
        }
        
    }
}
}
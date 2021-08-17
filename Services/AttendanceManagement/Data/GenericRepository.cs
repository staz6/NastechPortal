using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceManagement.Dto;
using AttendanceManagement.Entities;
using AttendanceManagement.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
/// <summary>
/// Main respoistory for attendance managment microservices
/// </summary>
namespace AttendanceManagement.Data
{
    public class GenericRepository : IGenericRepository
    {
        private readonly AppDbContext _context;
        private readonly IRequestClient<GetUserEventRequest> _requestClient;
        private readonly ILogger<GenericRepository> _logger;

        public GenericRepository(AppDbContext context, IRequestClient<GetUserEventRequest> requestClient,
                ILogger<GenericRepository> logger)
        {
            _logger = logger;
            _requestClient = requestClient;

            _context = context;
        }
        /**
            Method for admin so he can confirm the employee leave request 
        */

        public async Task AdminApproveLeaveRequest(int id)
        {
            var result = await _context.Leaves.FindAsync(id);
            if (result == null) throw new Exception();
            try
            {
                result.Status = true;
                
                var leaveHistory = await _context.LeaveHistorys.
                    FirstOrDefaultAsync(x => x.UserId==result.UserId && x.Year == result.From.Year);
                if(leaveHistory == null)
                {
                    LeaveHistory obj = new LeaveHistory{
                        UserId=result.UserId,
                        Total=12,
                        Year = result.From.Year,
                        Remaining=(((result.Till-result.From).Days) - 12)
                    };
                    await _context.LeaveHistorys.AddAsync(obj);
                }
                else{

                    leaveHistory.Remaining=leaveHistory.Remaining-((result.Till-result.From).Days+1);
                }
                DateTime dateTime = result.From;
                while(dateTime <= result.Till)
                {
                    Attendance obj = new Attendance{
                        UserId=result.UserId,
                        Date=dateTime,
                        ShiftTiming="Leave",
                        Status="Leave"
                    };
                    
                    await _context.AddAsync(obj);
                    dateTime.AddDays(1);
                    
                }
                await SaveChanges();
            }
            catch
            {
                throw new Exception();
            }

        }


      

        /**
        * ! Deprecated method do not use
        */

        public async Task<List<GetAllLeaveRequestDto>> GetAllLeave()
        {
            var request = _requestClient.Create(new GetUserEventRequest { });
            var response = await request.GetResponse<GetUserEventResponse>();
            var usersInfo = response.Message.getUserResponse;

            var leavesList = _context.Leaves.ToList();

            var query = leavesList
            .Join(
                usersInfo,
                leavesList => leavesList.UserId,
                usersInfo => usersInfo.UserId,
                (leavesList, usersInfo) => new
                {
                    Id = leavesList.Id,
                    Name = usersInfo.Name,
                    Reason = leavesList.Reason,
                    From = leavesList.From,
                    Till = leavesList.Till,
                    Status = leavesList.Status,
                    DeductSalary = leavesList.DeductSalary
                }
            ).ToList();

            List<GetAllLeaveRequestDto> returnList = new List<GetAllLeaveRequestDto>();
            foreach (var items in query)
            {
                var a = new GetAllLeaveRequestDto
                {
                    Name = items.Name,
                    From = items.From,
                    Till = items.Till,
                    Reason = items.Reason,
                    Id = items.Id,
                    DeductSalary = items.DeductSalary,
                    Status = items.Status
                };
                returnList.Add(a);
            }
            return returnList;

        }

        /**
         Get employee Leave History
        */
        public async Task<GetLeaveHistoryDto> getLeaveHistory(string userId)
        {
            var result = await _context.LeaveHistorys.
                FirstOrDefaultAsync(x => x.UserId == userId && x.Year == DateTime.Now.Year);
            if (result != null)
            {
                return new GetLeaveHistoryDto
                {
                    Total = result.Total,
                    Taken = result.Total - result.Remaining,
                    Remaining = result.Remaining,
                    Balance = result.Remaining,
                };
            }
            else
            {
                return new GetLeaveHistoryDto
                {
                    Total = 12,
                    Taken = 0,
                    Remaining = 12,
                    Balance = 12,
                };
            }
        }


        /**
        Get employee attendance
        */
        public async Task<List<Attendance>> getUserAttendance(string userId)
        {
            try
            {
                var getObject = await _context.Attendances.Where(x => x.UserId == userId).ToListAsync();
                return getObject;
            }
            catch
            {
                throw new Exception();
            }

        }

        /**
            Get employee attendance of specific month
        * TODO: Use this method somewhere 
        */
        public async Task<List<Attendance>> getUserAttendanceByMonth(string userId, DateTime month)
        {
            var getObject = await _context.Attendances.Where(x => x.UserId == userId
             && x.Date.Year == month.Year && x.Date.Month == month.Month).ToListAsync();
            return getObject;
        }

        /// <summary>
        /// Get employee Leave
        /// </summary>
        public async Task<List<Leave>> GetUserLeave(string userId)
        {
            var getObject = await _context.Leaves.Where(x => x.UserId == userId).ToListAsync();
            return getObject;
        }
        /// <summary>
        /// Post method for posting leave 
        /// </summary>
        public async Task leaveRequest(Leave model)
        {
            try
            {
                await _context.Leaves.AddAsync(model);
                await SaveChanges();
            }
            catch
            {
                throw new Exception();
            }

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
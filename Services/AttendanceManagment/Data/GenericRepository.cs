using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceManagment.Dto;
using AttendanceManagment.Entities;
using AttendanceManagment.Interface;
using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AttendanceManagment.Data
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

        public async Task AdminEditLeaveRequest(int id, AdminEditLeaveRequest model)
        {
            var result = await _context.Leaves.FindAsync(id);
            result.Status=model.Status;
            result.DeductSalary=model.DeductSalary;
            await SaveChanges();
        }

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
                    Id=leavesList.Id,
                    Name = usersInfo.Name,
                    Reason = leavesList.Reason,
                    From = leavesList.From,
                    Till = leavesList.Till,
                    Status = leavesList.Status,
                    DeductSalary = leavesList.DeductSalary
                }
            ).ToList();

            List<GetAllLeaveRequestDto> returnList = new List<GetAllLeaveRequestDto>();
            foreach(var items in query)
            {
                var a = new GetAllLeaveRequestDto{
                    Name = items.Name,
                    From=items.From,
                    Till=items.Till,
                    Reason=items.Reason,
                    Id=items.Id,
                    DeductSalary=items.DeductSalary,
                    Status = items.Status
                };
                returnList.Add(a);   
            }
            return returnList;
            
        }

        public async Task<List<Attendance>> getUserAttendance(string userId)
        {
            var getObject = await _context.Attendances.Where(x => x.UserId == userId).ToListAsync();
            return getObject;
        }

        public async Task<List<Leave>> GetUserLeave(string userId)
        {
            var getObject = await _context.Leaves.Where(x => x.UserId == userId).ToListAsync();
            return getObject;
        }

        public async Task leaveRequest(Leave model)
        {
            await _context.Leaves.AddAsync(model);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
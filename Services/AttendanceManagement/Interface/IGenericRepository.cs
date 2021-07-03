using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceManagement.Dto;
using AttendanceManagement.Entities;
using EventBus.Messages.Models;

namespace AttendanceManagement.Interface
{
    public interface IGenericRepository
    {
        

        Task<List<Attendance>> getUserAttendance(string userId);
        Task<List<Attendance>> getUserAttendanceByMonth(string userId,DateTime month);
        Task leaveRequest(Leave model);
        Task<List<Leave>> GetUserLeave(string userId);
        Task<List<GetAllLeaveRequestDto>> GetAllLeave();
        Task AdminEditLeaveRequest(int id, AdminEditLeaveRequest model);
        Task SaveChanges();
    }
}
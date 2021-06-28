using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceManagment.Dto;
using AttendanceManagment.Entities;
using EventBus.Messages.Models;

namespace AttendanceManagment.Interface
{
    public interface IGenericRepository
    {
        

        Task<List<Attendance>> getUserAttendance(string userId);

        Task leaveRequest(Leave model);
        Task<List<Leave>> GetUserLeave(string userId);
        Task<List<GetAllLeaveRequestDto>> GetAllLeave();
        Task SaveChanges();
    }
}
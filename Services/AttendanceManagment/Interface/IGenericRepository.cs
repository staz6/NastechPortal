using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceManagment.Entities;
using EventBus.Messages.Models;

namespace AttendanceManagment.Interface
{
    public interface IGenericRepository
    {
        Task CheckIn(Attendance model);

        Task CheckOut(Attendance model);

        Task<IEnumerable<Attendance>> getUserAttendance(string userId);

        Task leaveRequest(Leave model);

        Task SaveChanges();
    }
}
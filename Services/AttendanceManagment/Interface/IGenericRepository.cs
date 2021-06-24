using System.Threading.Tasks;
using AttendanceManagment.Entities;

namespace AttendanceManagment.Interface
{
    public interface IGenericRepository
    {
        Task CheckIn(Attendance model);

        Task CheckOut(Attendance model);

        Task SaveChanges();
    }
}
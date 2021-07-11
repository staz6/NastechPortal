using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Messages.Models;
using UserManagement.Dto;

using UserManagement.Entities;

namespace UserManagement.Interface
{
    /// <summary>
    /// Main Repository for Usermanageent service check AccountRepository in Data folder for actual implementaion
    /// </summary>
    public interface IAccountRepository
    {
        Task<int> RegisterUser(RegisterDto model);
        Task<string> Login(LoginDto model);
        Task<UsersInfoDto> getCurrentUser(string email);
        // Task<UserCheckInEventDto> CheckIn(string email);
        // Task<string> CheckOut(string email);

        Task<IReadOnlyList<Employee>> GetAllUser();

        Task<bool> getUserId(string email);

        Task GetAttendanceRecord(IEnumerable<GetAttendanceRecordEventDto> model);

        Task EditEditEmployeeInfo(string userId,EditEmployeeInfoDto model);
        Task<bool> BiometricCheck(int id);
        Task updateSalary(int id,UpdateSalaryDto model);
        
        
        
    }
}
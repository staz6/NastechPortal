using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Messages.Models;
using UserManagment.Dto;
using UserManagment.Dto.ServicesDto;
using UserManagment.Entities;

namespace UserManagment.Interface
{
    public interface IAccountRepository
    {
        Task<int> RegisterUser(RegisterDto model);
        Task<UserDto> Login(LoginDto model);
        Task<UsersInfoDto> getCurrentUser(string email);
        // Task<UserCheckInEventDto> CheckIn(string email);
        // Task<string> CheckOut(string email);

        Task<IReadOnlyList<Employee>> GetAllUser();

        Task<bool> getUserId(string email);

        Task GetAttendanceRecord(IEnumerable<GetAttendanceRecordEventDto> model);

        Task EditEditEmployeeInfo(string userId,EditEmployeeInfoDto model);
        Task<bool> BiometricCheck(int id);
        
        
        
    }
}
using System.Threading.Tasks;
using UserManagment.Dto;
using UserManagment.Dto.ServicesDto;
using UserManagment.Entities;

namespace UserManagment.Interface
{
    public interface IAccountRepository
    {
        Task RegisterUser(RegisterDto model);
        Task<UserDto> Login(LoginDto model);
        Task<UsersInfoDto> getCurrentUser(string email);
        Task<UserCheckInEventDto> CheckIn(string email);
        Task<string> CheckOut(string email);
    }
}
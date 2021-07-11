using UserManagement.Entities;

namespace UserManagement.Interface
{
    public interface ITokenService
    {
         string CreateToken(AppUser user,string roleName);
    }
}
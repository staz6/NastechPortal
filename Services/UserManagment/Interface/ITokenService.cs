using UserManagment.Entities;

namespace UserManagment.Interface
{
    public interface ITokenService
    {
         string CreateToken(AppUser user,string roleName);
    }
}
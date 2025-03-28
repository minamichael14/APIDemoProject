using APIDay1.DTO;
using APIDay1.Helpers;

namespace APIDay1.Services
{
    public interface IAuthService
    {

        RequestResult<bool> Register(UserDto user);
        RequestResult<string> Login(LoginDTO loginDTO);
        RequestResult<int> AddRole(RoleDto roleDTO);
        string GetRoleName(int roleID);
    }
}

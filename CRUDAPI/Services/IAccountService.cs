using CRUDAPI.Dtos;
using CRUDAPI.Entities.Helpers;

namespace CRUDAPI.Services;
public interface IAccountService
{
    string GenerateJwt(LoginDto dto);
    void RegisterUser(LoginDto dto, string role = RoleNames.User);
}
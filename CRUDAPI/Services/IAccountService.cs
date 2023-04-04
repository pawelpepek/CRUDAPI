using CRUDAPI.Dtos;

namespace CRUDAPI.Services;
public interface IAccountService
{
    string GenerateJwt(LoginDto dto);
    void RegisterUser(LoginDto dto, string role);
}
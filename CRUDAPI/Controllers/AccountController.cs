using CRUDAPI.Dtos;
using CRUDAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public string Login([FromBody] LoginDto dto)
    {
        var token = _accountService.GenerateJwt(dto);
        return token;
    }

    [HttpPost("register")]
    public void RegisterUser([FromBody] LoginDto dto)
    {
        _accountService.RegisterUser(dto);
    }
}

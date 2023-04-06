using CRUDAPI.Configuration;
using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Entities.Helpers;
using CRUDAPI.Infrastructure;
using CrudCore.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDAPI.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    public AccountService(ApplicationDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }
    public string GenerateJwt(LoginDto dto)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Login == dto.Login);
        if (user is null)
        {
            throw new CustomException("Niepoprawne login lub hasło!");
        }
        switch (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password))
        {
            case PasswordVerificationResult.Success:
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Login),
                        new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                    };


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
                var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims,
                    expires: expires, signingCredentials: cred);

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            case PasswordVerificationResult.Failed:
            default:
                throw new CustomException("Niepoprawne login lub hasło!");
        }

    }
    public void RegisterUser(LoginDto dto, string role = RoleNames.User)
    {
        var roleId = _dbContext.Roles.First(r => r.Name == role).Id;

        var newUser = new User()
        {
            Login = dto.Login,
            PasswordHash = "x",
            RoleId = roleId
        };
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }
}

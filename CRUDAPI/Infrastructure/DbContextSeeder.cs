using CRUDAPI.Dtos;
using CRUDAPI.Entities;
using CRUDAPI.Entities.Helpers;
using CRUDAPI.Services;

namespace CRUDAPI.Infrastructure;

public class DbContextSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly IAccountService _accountService;

    public DbContextSeeder(ApplicationDbContext context, IAccountService accountService)
    {
        _context = context;
        _accountService = accountService;
    }
    public void SeedData()
    {
        if (!_context.Roles.Any())
        {
            var adminRole = new Role() { Name = RoleNames.Admin };

            _context.Roles.AddRange
            (
                adminRole,
                new Role() { Name = RoleNames.User }
            );
            _context.SaveChanges();
        }
        if (!_context.Users.Any())
        {
            var userCredential = new LoginDto() { Login = "Admin", Password = "admin" };
            _accountService.RegisterUser(userCredential, RoleNames.Admin);
        }
    }
}

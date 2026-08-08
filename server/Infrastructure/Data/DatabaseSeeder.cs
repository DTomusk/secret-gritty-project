using Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;

    public DatabaseSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        // Seed roles
        if (await _context.Roles.AnyAsync())
            return;

        var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
        _context.Roles.Add(adminRole);
        await _context.SaveChangesAsync();

        // Seed test admin user if there are no users
        if (await _context.Users.AnyAsync())
            return;

        var testAdmin = User.Create("David");
        testAdmin.ActivateUser("$2a$11$hhBSsPx0fKrtGEKLB9JryeYCfrl940kRxnaRq4yLrY9Uxc46QVdbS");

        _context.Add(testAdmin);
        await _context.SaveChangesAsync();

        var adminUserRoleResult = UserRole.Create(testAdmin.Id, adminRole.Id);
        if (adminUserRoleResult.IsFailure)
        {
            throw new Exception($"Failed to create UserRole: {adminUserRoleResult.Error}");
        }
        _context.UserRoles.Add(adminUserRoleResult.Value);
        await _context.SaveChangesAsync();
    }
}

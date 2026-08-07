using Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Shared;

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

        var roles = new List<Role>
        {
            new Role { Name = "Admin" },
        };
        _context.Roles.AddRange(roles);
        await _context.SaveChangesAsync();

    }
}

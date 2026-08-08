using Application.Auth.Interfaces;
using Domain.Auth.Entities;
using Domain.Auth.ValueObjects;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        return user;
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserName == userName, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FindAsync([id], cancellationToken);
    }

    public async Task<User?> GetByRegistrationCodeAsync(RegistrationCode registrationCode, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.RegistrationCode == registrationCode, cancellationToken);
    }
}

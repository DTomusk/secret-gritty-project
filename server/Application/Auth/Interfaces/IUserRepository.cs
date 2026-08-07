using Domain.Auth.Entities;
using Domain.Auth.ValueObjects;

namespace Application.Auth.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameWithRolesAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByRegistrationCodeAsync(RegistrationCode registrationCode, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}

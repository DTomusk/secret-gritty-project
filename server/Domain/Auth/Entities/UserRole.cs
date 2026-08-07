using Domain.Shared.Results;

namespace Domain.Auth.Entities;

// UserRole is a join entity between User and Role
public class UserRole
{
    private UserRole() { }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;

    public static Result<UserRole> Create(Guid userId, Guid roleId)
    {
        if (userId == Guid.Empty)
            return Result<UserRole>.Failure(new Error("UserId cannot be empty.", ErrorType.Validation));
        if (roleId == Guid.Empty)
            return Result<UserRole>.Failure(new Error("RoleId cannot be empty.", ErrorType.Validation));
        return Result<UserRole>.Success(new UserRole
        {
            UserId = userId,
            RoleId = roleId
        });
    }
}

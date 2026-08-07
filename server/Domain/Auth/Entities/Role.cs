namespace Domain.Auth.Entities;

public class Role
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
}



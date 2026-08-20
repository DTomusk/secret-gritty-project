using Domain.Auth.ValueObjects;

namespace Domain.Auth.Entities;

// TODO: this crosses bounded contexts, consider having a separate User entity
public class User
{
    private string _userName = string.Empty;

    private User() { } // For EF Core

    public Guid Id { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;

    // Users are invited and must register with a code
    public RegistrationCode RegistrationCode { get; private set; }
    public bool IsActive { get; private set; }

    public string UserName
    {
        get => _userName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("User name cannot be empty or whitespace.", nameof(UserName));

            _userName = value;
        }
    }

    public DateTime CreatedAt { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    public static User Create(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("User name cannot be empty or whitespace.", nameof(userName));

        return new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            PasswordHash = "",
            CreatedAt = DateTime.UtcNow,
            RegistrationCode = new RegistrationCode(),
        };
    }

    public void UpdateUserName(string newUserName)
    {
        if (string.IsNullOrWhiteSpace(newUserName))
            throw new ArgumentException("User name cannot be empty or whitespace.", nameof(newUserName));

        UserName = newUserName;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty or whitespace.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void ActivateUser(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty or whitespace.", nameof(passwordHash));
        PasswordHash = passwordHash;
        IsActive = true;
    }

    public void AssignRole(Role role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role), "Role cannot be null.");
        if (UserRoles.Any(ur => ur.RoleId == role.Id))
            throw new InvalidOperationException("User already has this role assigned.");
        var result = UserRole.Create(this.Id, role.Id);
        if (result.IsFailure)
            throw new InvalidOperationException(result.Error.Message);
        UserRoles.Add(result.Value);
    }
}
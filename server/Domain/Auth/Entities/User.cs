namespace Domain.Auth.Entities;

public class User
{
    private string _userName = string.Empty;

    private User() { } // For EF Core

    public Guid Id { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;

    // Users are invited and must register with a code
    public string RegistrationCode { get; private set; }
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

    public static User Create(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("User name cannot be empty or whitespace.", nameof(userName));

        return new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            PasswordHash = "",
            CreatedAt = DateTime.UtcNow
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
}
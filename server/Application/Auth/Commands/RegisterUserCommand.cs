namespace Application.Auth.Commands;

public record RegisterUserCommand(string RegistrationCode, string Password);
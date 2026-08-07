namespace Application.Auth.DTOs;

public record AuthResponse(Guid UserId, string UserName, string Token);
using Application.Auth.DTOs;

namespace Application.Auth.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(UserTokenData tokenData);
}

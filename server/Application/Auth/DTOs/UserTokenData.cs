using Domain.Auth.Entities;

namespace Application.Auth.DTOs;

public record UserTokenData(Guid UserId, string UserName, IEnumerable<string> Roles)
{
    public static UserTokenData FromUser(User user)
    {
        var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();
        return new UserTokenData(user.Id, user.UserName, roles);
    }
}
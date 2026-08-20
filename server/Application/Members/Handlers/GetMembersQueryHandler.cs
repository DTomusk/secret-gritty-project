using Application.Auth.Interfaces;
using Application.Members.DTOs;
using Application.Members.Queries;
using Application.Shared.Interfaces;

namespace Application.Members.Handlers;

public class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, IEnumerable<MemberDTO>>
{
    private readonly IUserRepository _userRepository;

    public GetMembersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<MemberDTO>> HandleAsync(GetMembersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users.Select(user => new MemberDTO(user.Id, user.UserName));
    }
}

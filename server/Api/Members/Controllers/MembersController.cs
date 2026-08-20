using Api.Members.DTOs;
using Api.Shared.Controllers;
using Application.Auth.Interfaces;
using Application.Members.DTOs;
using Application.Members.Queries;
using Application.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Members.Controllers;

[Route("Members")]
[ApiController]
public class MembersController : AuthenticatedControllerBase
{
    private readonly IQueryHandler<GetMembersQuery, IEnumerable<MemberDTO>> _getMembersQueryHandler;
    public MembersController(ICurrentUserService currentUserService, IQueryHandler<GetMembersQuery, IEnumerable<MemberDTO>> getMembersQueryHandler) : base(currentUserService)
    {
        _getMembersQueryHandler = getMembersQueryHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _getMembersQueryHandler.HandleAsync(new GetMembersQuery());
        return Ok(new GetMembersResponse(members));
    }
}

using Application.Members.DTOs;

namespace Api.Members.DTOs;

public record GetMembersResponse(IEnumerable<MemberDTO> Members);
using Api.CalendarEvents.DTOs;
using Api.Shared.Controllers;
using Api.Shared.RateLimiting;
using Application.Auth.Interfaces;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.CalendarEvents.Controllers;

[Authorize]
[ApiController]
[EnableRateLimiting(RateLimitingConfiguration.AuthenticatedPolicy)]
public class EventPollsController : AuthenticatedControllerBase
{
    private readonly ICommandHandler<CreateEventPollCommand, CreateEventPollResponse> _createEventPollHandler;

    public EventPollsController(ICommandHandler<CreateEventPollCommand, CreateEventPollResponse> createEventPollHandler,
        ICurrentUserService currentUserService) : base(currentUserService)
    {
        _createEventPollHandler = createEventPollHandler;
    }

    [HttpPost("Events/{eventId}/Polls", Name = "CreateEventPoll")]
    public async Task<IActionResult> CreateEventPoll(Guid eventId, [FromBody] CreateEventPollRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateEventPollCommand(eventId, CurrentUserId, request.PollType, request.ClosesAt);
        var result = await _createEventPollHandler.HandleAsync(command);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

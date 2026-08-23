using Api.CalendarEvents.DTOs;
using Api.Shared.Controllers;
using Api.Shared.RateLimiting;
using Application.Auth.Interfaces;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Queries;
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
    private readonly IQueryHandler<EventPollsQuery, IEnumerable<EventPollResponse>> _getEventPollsHandler;

    public EventPollsController(ICommandHandler<CreateEventPollCommand, CreateEventPollResponse> createEventPollHandler,
        ICurrentUserService currentUserService, IQueryHandler<EventPollsQuery, IEnumerable<EventPollResponse>> getEventPollsHandler) : base(currentUserService)
    {
        _createEventPollHandler = createEventPollHandler;
        _getEventPollsHandler = getEventPollsHandler;
    }

    [HttpGet("Events/{eventId:guid}/Polls")]
    public async Task<IActionResult> GetEventPolls(Guid eventId, CancellationToken cancellationToken)
    {
        var query = new EventPollsQuery(eventId);
        var polls = await _getEventPollsHandler.HandleAsync(query, cancellationToken);
        return Ok(polls);
    }

    [HttpPost("Events/{eventId:guid}/Polls", Name = "CreateEventPoll")]
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

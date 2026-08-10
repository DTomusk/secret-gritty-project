using Api.CalendarEvents.DTOs;
using Api.Shared.Controllers;
using Api.Shared.Extensions;
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
[Route("Events")]
[ApiController]
[EnableRateLimiting(RateLimitingConfiguration.AuthenticatedPolicy)]
public class CalendarEventsController : AuthenticatedControllerBase
{
    private readonly ICommandHandler<ScheduleEventCommand, ScheduleEventResponse> _scheduleEventCommandHandler;
    private readonly IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?> _upcomingEventQueryHandler;

    public CalendarEventsController(ICommandHandler<ScheduleEventCommand, ScheduleEventResponse> scheduleEventCommandHandler,
        IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?> upcomingEventQueryHandler,
        ICurrentUserService currentUserService) : base(currentUserService)
    {
        _scheduleEventCommandHandler = scheduleEventCommandHandler;
        _upcomingEventQueryHandler = upcomingEventQueryHandler;
    }

    [HttpGet("Next", Name = "GetUpcomingEvent")]
    public async Task<IActionResult> GetUpcomingEvent()
    {
        var query = new UpcomingEventQuery();
        var upcomingEvent = await _upcomingEventQueryHandler.HandleAsync(query);
        return upcomingEvent is null ? NoContent() : Ok(upcomingEvent);
    }

    [HttpGet("{id:guid}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var query = new EventByIdQuery(id);
    }

    [HttpPost(Name = "ScheduleEvent")]
    public async Task<IActionResult> ScheduleEvent([FromBody] ScheduleEventRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ScheduleEventCommand(
            CurrentUserId,
            request.Name,
            request.Date,
            request.EventType
            );

        var result = await _scheduleEventCommandHandler.HandleAsync(command);

        return result.ToActionResult();
    }
}

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
    private readonly ICommandHandler<ChooseNextHostCommand> _chooseNextHostCommandHandler;
    private readonly IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?> _upcomingEventQueryHandler;
    private readonly IQueryHandler<UpcomingBookClubQuery, UpcomingEventResponse?> _upcomingBookClubQueryHandler;
    private readonly IQueryHandler<EventByIdQuery, EventDetailResponse?> _eventByIdQueryHandler;

    public CalendarEventsController(ICommandHandler<ScheduleEventCommand, ScheduleEventResponse> scheduleEventCommandHandler,
        ICommandHandler<ChooseNextHostCommand> chooseNextHostCommandHandler,
        IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?> upcomingEventQueryHandler,
        IQueryHandler<UpcomingBookClubQuery, UpcomingEventResponse?> upcomingBookClubQueryHandler,
        IQueryHandler<EventByIdQuery, EventDetailResponse?> eventByIdQueryHandler,
        ICurrentUserService currentUserService) : base(currentUserService)
    {
        _scheduleEventCommandHandler = scheduleEventCommandHandler;
        _chooseNextHostCommandHandler = chooseNextHostCommandHandler;
        _upcomingEventQueryHandler = upcomingEventQueryHandler;
        _upcomingBookClubQueryHandler = upcomingBookClubQueryHandler;
        _eventByIdQueryHandler = eventByIdQueryHandler;
    }

    [HttpGet("Next", Name = "GetUpcomingEvent")]
    public async Task<IActionResult> GetUpcomingEvent()
    {
        var query = new UpcomingEventQuery();
        var upcomingEvent = await _upcomingEventQueryHandler.HandleAsync(query);
        return upcomingEvent is null ? NoContent() : Ok(upcomingEvent);
    }

    [HttpGet("Next/BookClub", Name = "GetUpcomingBookClubEvent")]
    public async Task<IActionResult> GetUpcomingBookClubEvent()
    {
        var query = new UpcomingBookClubQuery();
        var upcomingEvent = await _upcomingBookClubQueryHandler.HandleAsync(query);
        return upcomingEvent is null ? NoContent() : Ok(upcomingEvent);
    }

    [HttpPost("Next/BookClub/Host", Name = "ScheduleBookClubEvent")]
    public async Task<IActionResult> ScheduleBookClubEvent([FromBody] ChooseNextHostRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new ChooseNextHostCommand(request.HostId);

        var result = await _chooseNextHostCommandHandler.HandleAsync(command);

        return result.ToActionResult();
    }

    [HttpGet("{id:guid}", Name = "GetEventById")]
    public async Task<IActionResult> GetEventById(Guid id)
    {
        var query = new EventByIdQuery(id);
        var eventDetail = await _eventByIdQueryHandler.HandleAsync(query);
        return eventDetail is null ? NotFound() : Ok(eventDetail);
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

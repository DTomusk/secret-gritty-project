using Api.CalendarEvents.DTOs;
using Api.Shared.Controllers;
using Api.Shared.Extensions;
using Api.Shared.RateLimiting;
using Application.Auth.Interfaces;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.CalendarEvents.Controllers;

[Route("[controller]")]
[ApiController]
[EnableRateLimiting(RateLimitingConfiguration.AuthenticatedPolicy)]
public class CalendarEventsController : AuthenticatedControllerBase
{
    private readonly ICommandHandler<ScheduleEventCommand, ScheduleEventResponse> _scheduleEventCommandHandler;

    public CalendarEventsController(ICommandHandler<ScheduleEventCommand, ScheduleEventResponse> scheduleEventCommandHandler,
        ICurrentUserService currentUserService) : base(currentUserService)
    {
        _scheduleEventCommandHandler = scheduleEventCommandHandler;
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

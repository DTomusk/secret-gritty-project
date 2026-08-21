using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;

namespace Application.CalendarEvents.Handlers;

public class UpcomingEventQueryHandler : IQueryHandler<UpcomingEventQuery, UpcomingEventResponse?>
{
    private readonly ICalendarEventQueryService _calendarEventQueryService;

    public UpcomingEventQueryHandler(ICalendarEventQueryService calendarEventQueryService)
    {
        _calendarEventQueryService = calendarEventQueryService;
    }

    public async Task<UpcomingEventResponse?> HandleAsync(UpcomingEventQuery query, CancellationToken cancellationToken = default)
    {
        return await _calendarEventQueryService.GetUpcomingEventAsync(query.UserId, cancellationToken);
    }
}

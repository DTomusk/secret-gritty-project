using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;

namespace Application.CalendarEvents.Handlers;

public class UpcomingBookClubQueryHandler : IQueryHandler<UpcomingBookClubQuery, UpcomingEventResponse?>
{
    private readonly ICalendarEventQueryService _calendarEventQueryService;

    public UpcomingBookClubQueryHandler(ICalendarEventQueryService calendarEventQueryService)
    {
        _calendarEventQueryService = calendarEventQueryService;
    }

    public async Task<UpcomingEventResponse?> HandleAsync(UpcomingBookClubQuery query, CancellationToken cancellationToken = default)
    {
        // We have a couple of options: 
        // There is no upcoming book club event, in which case a host needs to be chosen 
        // These is an upcoming book club event, but there is no date, so, unless you're the host, you have to wait 
        // There is an upcoming book club event with a date
        // Note: there can only ever be one upcoming book club event
        var upcomingEvent = await _calendarEventQueryService.GetUpcomingBookClubEventAsync(cancellationToken);
        if (upcomingEvent != null)
            return upcomingEvent;

        var unscheduledEvent = await _calendarEventQueryService.GetUnscheduledBookClubEventAsync(cancellationToken);
        if (unscheduledEvent != null)
            return unscheduledEvent;

        return null;
    }
}

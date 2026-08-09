using Application.CalendarEvents.DTOs;

namespace Application.CalendarEvents.Interfaces;

public interface ICalendarEventQueryService
{
    Task<UpcomingEventResponse?> GetUpcomingEventAsync();
}

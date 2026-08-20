using Application.CalendarEvents.DTOs;

namespace Application.CalendarEvents.Interfaces;

public interface ICalendarEventQueryService
{
    Task<UpcomingEventResponse?> GetUpcomingEventAsync(CancellationToken cancellationToken = default);
    Task<EventDetailResponse?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

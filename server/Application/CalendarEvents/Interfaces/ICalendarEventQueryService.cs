using Application.CalendarEvents.DTOs;

namespace Application.CalendarEvents.Interfaces;

public interface ICalendarEventQueryService
{
    Task<UpcomingEventResponse?> GetUpcomingEventAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<EventDetailResponse?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UpcomingEventResponse?> GetUpcomingBookClubEventAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UpcomingEventResponse?> GetUnscheduledBookClubEventAsync(Guid userId, CancellationToken cancellationToken = default);
}

using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Interfaces;

public interface ICalendarEventRepository
{
    Task CreateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
    Task<CalendarEvent?> GetFutureEventByType(CalendarEventType eventType, CancellationToken cancellationToken = default);
    Task<CalendarEvent?> GetUnscheduledEventByType(CalendarEventType eventType, CancellationToken cancellationToken = default);
}

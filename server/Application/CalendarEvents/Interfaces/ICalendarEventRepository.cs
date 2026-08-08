using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Interfaces;

public interface ICalendarEventRepository
{
    Task CreateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default);
}

using Application.CalendarEvents.Interfaces;
using Domain.CalendarEvents.Entities;
using Infrastructure.Data;

namespace Infrastructure.CalendarEvents.Repositories;

public class CalendarEventRespository : ICalendarEventRepository
{
    private readonly AppDbContext _context;

    public CalendarEventRespository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CalendarEvent calendarEvent, CancellationToken cancellationToken = default)
    {
        _context.CalendarEvents.Add(calendarEvent);
    }

    public async Task<CalendarEvent?> GetFutureEventByType(CalendarEventType eventType, CancellationToken cancellationToken = default)
    {
        return _context.CalendarEvents
            .Where(e => e.EventType == eventType && e.Date.HasValue && e.Date > DateOnly.FromDateTime(DateTime.UtcNow))
            .OrderBy(e => e.Date)
            .FirstOrDefault();
    }

    public async Task<CalendarEvent?> GetUnscheduledEventByType(CalendarEventType eventType, CancellationToken cancellationToken = default)
    {
        return _context.CalendarEvents
            .Where(e => e.EventType == eventType && !e.Date.HasValue)
            .OrderBy(e => e.Date)
            .FirstOrDefault();
    }
}

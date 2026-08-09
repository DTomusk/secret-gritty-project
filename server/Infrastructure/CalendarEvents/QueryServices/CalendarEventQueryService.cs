using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CalendarEvents.QueryServices;

public class CalendarEventQueryService : ICalendarEventQueryService
{
    private readonly AppDbContext _context;

    public CalendarEventQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UpcomingEventResponse?> GetUpcomingEventAsync()
    {
        return await _context.CalendarEvents
            .Join(
                _context.Users,
                calendarEvent => calendarEvent.ScheduledByUserId,
                user => user.Id,
                (calendarEvent, user) => new { calendarEvent, user }
            )
            .Where(x => x.calendarEvent.Date > DateOnly.FromDateTime(DateTime.UtcNow))
            // Order by ascending date
            .OrderBy(x => x.calendarEvent.Date)
            .Select(x => new UpcomingEventResponse(
                x.calendarEvent.Name,
                x.calendarEvent.Date,
                x.calendarEvent.EventType,
                x.user.UserName
            ))
            .FirstOrDefaultAsync();
    }
}

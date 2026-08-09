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
                (calendarEvent, user) => new UpcomingEventResponse(
                    calendarEvent.Name,
                    calendarEvent.Date,
                    calendarEvent.EventType,
                    user.UserName
                    )
             )
            .Where(e => e.Date > DateOnly.FromDateTime(DateTime.UtcNow))
            // Order by ascending date
            .OrderBy(e => e.Date)
            .FirstOrDefaultAsync();
    }
}

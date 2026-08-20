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
}

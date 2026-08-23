using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Domain.CalendarEvents.Entities;
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

    public async Task<UpcomingEventResponse?> GetUpcomingEventAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.CalendarEvents
            .AsNoTracking()
            .Join(
                _context.Users,
                calendarEvent => calendarEvent.HostUserId,
                user => user.Id,
                (calendarEvent, user) => new { calendarEvent, user }
            )
            .Where(x => x.calendarEvent.Date > DateOnly.FromDateTime(DateTime.UtcNow))
            // Order by ascending date
            .OrderBy(x => x.calendarEvent.Date)
            .Select(x => new UpcomingEventResponse(
                x.calendarEvent.Id,
                x.calendarEvent.Name,
                x.calendarEvent.Date,
                x.calendarEvent.EventType,
                x.user.UserName,
                x.user.Id == userId
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<UpcomingEventResponse?> GetUpcomingBookClubEventAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.CalendarEvents
            .AsNoTracking()
            .Join(
                _context.Users,
                calendarEvent => calendarEvent.HostUserId,
                user => user.Id,
                (calendarEvent, user) => new { calendarEvent, user }
            )
            .Where(x => x.calendarEvent.Date.HasValue
                && x.calendarEvent.Date > DateOnly.FromDateTime(DateTime.UtcNow)
                && x.calendarEvent.EventType == CalendarEventType.BookClub)
            .Select(x => new UpcomingEventResponse(
                x.calendarEvent.Id,
                x.calendarEvent.Name,
                x.calendarEvent.Date,
                x.calendarEvent.EventType,
                x.user.UserName,
                x.user.Id == userId
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<UpcomingEventResponse?> GetUnscheduledBookClubEventAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.CalendarEvents
            .AsNoTracking()
            .Join(
                _context.Users,
                calendarEvent => calendarEvent.HostUserId,
                user => user.Id,
                (calendarEvent, user) => new { calendarEvent, user }
            )
            .Where(x => !x.calendarEvent.Date.HasValue
                && x.calendarEvent.EventType == CalendarEventType.BookClub)
            .Select(x => new UpcomingEventResponse(
                x.calendarEvent.Id,
                x.calendarEvent.Name,
                x.calendarEvent.Date,
                x.calendarEvent.EventType,
                x.user.UserName,
                x.user.Id == userId
            ))
            .FirstOrDefaultAsync();
    }

    public async Task<EventDetailResponse?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CalendarEvents
           .AsNoTracking()
           .Join(
               _context.Users,
               calendarEvent => calendarEvent.HostUserId,
               user => user.Id,
               (calendarEvent, user) => new { calendarEvent, user }
           )
           .Where(x => x.calendarEvent.Id == id)
           .Select(x => new EventDetailResponse(
               x.calendarEvent.Name,
               x.calendarEvent.Date,
               x.calendarEvent.EventType,
               x.user.UserName
           ))
           .FirstOrDefaultAsync();
    }

}

using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CalendarEvents.QueryServices;

public class EventPollQueryService : IEventPollQueryService
{
    private readonly AppDbContext _context;

    public EventPollQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventPollResponse>> GetEventPollsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.Polls
            .AsNoTracking()
            .Where(p => p.EventId == eventId)
            .Select(p => new EventPollResponse(
                p.Id,
                p.ClosesAt,
                p.Options.Select(o => new EventPollOptionResponse(o.Id, o.Value))
            ))
            .ToListAsync(cancellationToken);
    }
}

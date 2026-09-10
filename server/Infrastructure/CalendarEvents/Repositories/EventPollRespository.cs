using Application.CalendarEvents.Interfaces;
using Domain.CalendarEvents.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CalendarEvents.Repositories;

public class EventPollRepository : IEventPollRepository
{
    private readonly AppDbContext _context;

    public EventPollRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateEventPollAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        _context.Polls.Add(poll);
    }

    public async Task<Poll?> GetEventPollByPollIdAsync(Guid pollId, CancellationToken cancellationToken = default)
    {
        return await _context.Polls
            .Include(p => p.Options)
            .FirstOrDefaultAsync(p => p.Id == pollId, cancellationToken);
    }

    public async Task<IEnumerable<Poll>> GetEventPollsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        return await _context.Polls
            .Where(p => p.EventId == eventId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateEventPollAsync(Poll poll, CancellationToken cancellationToken = default)
    {
        _context.Polls.Update(poll);
    }
}

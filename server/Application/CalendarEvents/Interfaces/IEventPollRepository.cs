using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Interfaces;

public interface IEventPollRepository
{
    Task<IEnumerable<Poll>> GetEventPollsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task<Poll?> GetEventPollByPollIdAsync(Guid pollId, CancellationToken cancellationToken = default);
    Task CreateEventPollAsync(Poll poll, CancellationToken cancellationToken = default);
    Task UpdateEventPollAsync(Poll poll, CancellationToken cancellationToken = default);
}

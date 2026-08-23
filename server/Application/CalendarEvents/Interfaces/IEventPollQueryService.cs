using Application.CalendarEvents.DTOs;

namespace Application.CalendarEvents.Interfaces;

public interface IEventPollQueryService
{
    Task<IEnumerable<EventPollResponse>> GetEventPollsByEventIdAsync(Guid eventId, CancellationToken cancellationToken = default);
}

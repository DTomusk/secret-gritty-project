using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;

namespace Application.CalendarEvents.Handlers;

public class EventPollsQueryHandler : IQueryHandler<EventPollsQuery, IEnumerable<EventPollResponse>>
{
    public Task<IEnumerable<EventPollResponse>> HandleAsync(EventPollsQuery query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

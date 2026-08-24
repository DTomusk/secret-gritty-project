using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;

namespace Application.CalendarEvents.Handlers;

public class EventPollsQueryHandler : IQueryHandler<EventPollsQuery, IEnumerable<EventPollResponse>>
{
    private readonly IEventPollQueryService _eventPollQueryService;

    public EventPollsQueryHandler(IEventPollQueryService eventPollQueryService)
    {
        _eventPollQueryService = eventPollQueryService;
    }

    public async Task<IEnumerable<EventPollResponse>> HandleAsync(EventPollsQuery query, CancellationToken cancellationToken = default)
    {
        return await _eventPollQueryService.GetEventPollsByEventIdAsync(query.EventId, cancellationToken);
    }
}

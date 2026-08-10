using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.CalendarEvents.Queries;
using Application.Shared.Interfaces;

namespace Application.CalendarEvents.Handlers;

public class EventByIdQueryHandler : IQueryHandler<EventByIdQuery, EventDetailResponse>
{
    private readonly ICalendarEventQueryService _queryService;

    public EventByIdQueryHandler(ICalendarEventQueryService queryService)
    {
        _queryService = queryService;
    }

    public Task<EventDetailResponse> HandleAsync(EventByIdQuery query, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

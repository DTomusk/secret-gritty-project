using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Commands;

public record CreateEventPollCommand(Guid EventId, Guid UserId, PollType PollType, DateTime ClosesAt, CreateEventPollOptionsRequest[] Options);

public record CreateEventPollOptionsRequest
{
    public string? Location { get; init; }
    public DateTime? Date { get; init; }
    public string? Title { get; init; }
    public string? Author { get; init; }
}
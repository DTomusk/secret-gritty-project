using Domain.CalendarEvents.Entities;

namespace Api.CalendarEvents.DTOs;

public record CreateEventPollRequest(
    PollType PollType,
    DateTime ClosesAt,
    PollOptionsRequest[] Options);

public record PollOptionsRequest
{
    public string? Location { get; init; }
    public DateTime? Date { get; init; }
    public string? Title { get; init; }
    public string? Author { get; init; }
}
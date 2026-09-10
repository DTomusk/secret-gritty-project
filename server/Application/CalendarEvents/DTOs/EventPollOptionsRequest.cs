namespace Application.CalendarEvents.DTOs;

public record EventPollOptionsRequest
{
    public string? Location { get; init; }
    public DateTime? Date { get; init; }
    public string? Title { get; init; }
    public string? Author { get; init; }
}

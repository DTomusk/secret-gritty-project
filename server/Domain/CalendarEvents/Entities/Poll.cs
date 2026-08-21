namespace Domain.CalendarEvents.Entities;

public class Poll
{
    public Guid Id { get; init; }
    public Guid EventId { get; init; }
    public PollType Type { get; init; }
    public DateTime ClosesAt { get; init; }

    private Poll() { }

    public static Poll Create(CalendarEvent @event, PollType type, DateTime closesAt)
    {
        if (closesAt <= DateTime.UtcNow)
            throw new ArgumentException("Poll must close in the future");

        if (type == PollType.Book && @event.EventType != CalendarEventType.BookClub)
            throw new ArgumentException("Book polls can only be made for book club events");

        return new Poll
        {
            Id = Guid.NewGuid(),
            EventId = @event.Id,
            Type = type,
            ClosesAt = closesAt
        };
    }
}

public enum PollType
{
    Date = 1,
    Location = 2,
    Book = 3,
}

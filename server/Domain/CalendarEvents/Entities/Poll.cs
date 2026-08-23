namespace Domain.CalendarEvents.Entities;

public class Poll
{
    public Guid Id { get; init; }
    public Guid EventId { get; init; }
    public PollType Type { get; init; }
    public DateTime ClosesAt { get; init; }
    public ICollection<PollOption> Options { get; init; } = new List<PollOption>();

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
            ClosesAt = closesAt,
            Options = new List<PollOption>()
        };
    }

    public void AddOption(PollOption option)
    {
        if (this.Type != option.Type)
            throw new ArgumentException("Option type does not match poll type");
        Options.Add(option);
    }
}

public enum PollType
{
    Date = 1,
    Location = 2,
    Book = 3,
}

public class PollOption
{
    public Guid Id { get; init; }
    public PollType Type { get; init; }
    public string Value { get; init; }

    private PollOption() { }

    public static PollOption CreateLocationOption(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty");

        return new PollOption
        {
            Id = Guid.NewGuid(),
            Type = PollType.Location,
            Value = location
        };
    }

    public static PollOption CreateDateOption(DateTime date)
    {
        if (date <= DateTime.UtcNow)
            throw new ArgumentException("Date must be in the future");
        return new PollOption
        {
            Id = Guid.NewGuid(),
            Type = PollType.Date,
            Value = date.ToString("o") // ISO 8601 format
        };
    }

    public static PollOption CreateBookOption(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty");
        return new PollOption
        {
            Id = Guid.NewGuid(),
            Type = PollType.Book,
            Value = $"{title} by {author}"
        };
    }
}
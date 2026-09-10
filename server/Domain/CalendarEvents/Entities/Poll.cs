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
        var closesAtUtc = closesAt.Kind == DateTimeKind.Local
        ? closesAt.ToUniversalTime()
        : closesAt.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(closesAt, DateTimeKind.Utc)
            : closesAt;

        if (closesAtUtc <= DateTime.UtcNow)
            throw new ArgumentException("Poll must close in the future");

        if (type == PollType.Book && @event.EventType != CalendarEventType.BookClub)
            throw new ArgumentException("Book polls can only be made for book club events");

        return new Poll
        {
            Id = Guid.NewGuid(),
            EventId = @event.Id,
            Type = type,
            ClosesAt = closesAtUtc,
            Options = new List<PollOption>()
        };
    }

    public void AddDateOption(DateTime date)
    {
        if (this.Type != PollType.Date)
            throw new ArgumentException("Option type does not match poll type");
        Options.Add(PollOption.CreateDateOption(this.Id, date));
    }

    public void AddBookOption(string title, string author)
    {
        if (this.Type != PollType.Book)
            throw new ArgumentException("Option type does not match poll type");
        Options.Add(PollOption.CreateBookOption(this.Id, title, author));
    }

    public void AddLocationOption(string location)
    {
        if (this.Type != PollType.Location)
            throw new ArgumentException("Option type does not match poll type");
        Options.Add(PollOption.CreateLocationOption(this.Id, location));
    }

    public void DeleteOptions()
    {
        Options.Clear();
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
    public Guid PollId { get; init; }
    public PollType Type { get; init; }
    public string Value { get; init; }

    private PollOption() { }

    public static PollOption CreateLocationOption(Guid pollId, string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be empty");

        return new PollOption
        {
            Id = Guid.NewGuid(),
            PollId = pollId,
            Type = PollType.Location,
            Value = location
        };
    }

    public static PollOption CreateDateOption(Guid pollId, DateTime date)
    {
        if (date <= DateTime.UtcNow)
            throw new ArgumentException("Date must be in the future");
        return new PollOption
        {
            Id = Guid.NewGuid(),
            PollId = pollId,
            Type = PollType.Date,
            Value = date.ToString("o") // ISO 8601 format
        };
    }

    public static PollOption CreateBookOption(Guid pollId, string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty");
        return new PollOption
        {
            Id = Guid.NewGuid(),
            PollId = pollId,
            Type = PollType.Book,
            Value = $"{title} by {author}"
        };
    }
}
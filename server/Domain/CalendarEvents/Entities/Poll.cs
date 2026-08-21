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
        if (!OptionValid(option))
            throw new ArgumentException("Option invalid for this poll type");

        Options.Add(option);
    }

    private bool OptionValid(PollOption option)
    {
        if (this.Type == PollType.Book && option is not PollBookOption)
            return false;

        if (this.Type == PollType.Location && option is not PollLocationOption)
            return false;

        if (this.Type == PollType.Date && option is not PollDateOption)
            return false;

        return true;
    }
}

public enum PollType
{
    Date = 1,
    Location = 2,
    Book = 3,
}

public abstract class PollOption
{
    public Guid Id { get; init; }
}

public class PollLocationOption : PollOption
{
    public string Location { get; init; }
}

public class PollDateOption : PollOption
{
    public DateTime Date { get; init; }
}

public class PollBookOption : PollOption
{
    public string Title { get; init; }
    public string Author { get; init; }
}

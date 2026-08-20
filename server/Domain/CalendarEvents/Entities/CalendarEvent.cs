namespace Domain.CalendarEvents.Entities;

public class CalendarEvent
{
    public Guid Id { get; init; }
    public Guid ScheduledByUserId { get; init; }
    public string Name { get; private set; }
    public DateOnly Date { get; private set; }
    public CalendarEventType EventType { get; private set; }

    private CalendarEvent() { }

    public static CalendarEvent Create(Guid scheduledByUserId, string name, DateOnly date, CalendarEventType eventType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return new CalendarEvent
        {
            Id = Guid.NewGuid(),
            ScheduledByUserId = scheduledByUserId,
            Name = name,
            Date = date,
            EventType = eventType
        };
    }
}

public enum CalendarEventType
{
    BookClub = 1
}

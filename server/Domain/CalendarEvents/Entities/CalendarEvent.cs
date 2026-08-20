namespace Domain.CalendarEvents.Entities;

public class CalendarEvent
{
    /// <summary>
    /// Unique identifier for the calendar event.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The Id of the event host
    /// </summary>
    public Guid HostUserId { get; init; }

    /// <summary>
    /// The name of the calendar event.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The date of the event. Can be null if the date hasn't been decided yet, but the host has to set it
    /// </summary>
    public DateOnly? Date { get; private set; }

    /// <summary>
    /// The type of the calendar event.
    /// </summary>
    public CalendarEventType EventType { get; private set; }

    private CalendarEvent() { }

    public static CalendarEvent CreateUnscheduledEvent(Guid hostUserId, string name, CalendarEventType eventType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        return new CalendarEvent
        {
            Id = Guid.NewGuid(),
            HostUserId = hostUserId,
            Name = name,
            Date = null,
            EventType = eventType
        };
    }

    public static CalendarEvent CreateScheduledEvent(Guid hostUserId, string name, DateOnly date, CalendarEventType eventType)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
        }

        if (date <= DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException("Event date must be in the future.", nameof(date));
        }

        return new CalendarEvent
        {
            Id = Guid.NewGuid(),
            HostUserId = hostUserId,
            Name = name,
            Date = date,
            EventType = eventType
        };
    }

    public void ScheduleEvent(DateOnly date)
    {
        if (date <= DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException("Event date must be in the future.", nameof(date));
        }
        Date = date;
    }
}

/// <summary>
/// The types of calendar events that can be created
/// Currently, all events are book club
/// </summary>
public enum CalendarEventType
{
    BookClub = 1
}

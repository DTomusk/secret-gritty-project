using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Commands;

public record ScheduleEventCommand(Guid UserId, string Name, DateOnly Date, CalendarEventType EventType);
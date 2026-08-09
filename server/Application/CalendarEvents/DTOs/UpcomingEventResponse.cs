using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.DTOs;

public record UpcomingEventResponse(string Name, DateOnly Date, CalendarEventType EventType, string ScheduledByUserName);

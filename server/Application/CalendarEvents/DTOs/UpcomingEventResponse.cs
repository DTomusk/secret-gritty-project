using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.DTOs;

public record UpcomingEventResponse(Guid Id, string Name, DateOnly? Date, CalendarEventType EventType, string HostUserName);

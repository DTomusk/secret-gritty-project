using Domain.CalendarEvents.Entities;

namespace Api.CalendarEvents.DTOs;

public record ScheduleEventRequest(string Name, DateOnly Date, CalendarEventType EventType);
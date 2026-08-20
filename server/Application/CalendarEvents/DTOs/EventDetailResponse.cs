using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.DTOs;

// TODO: event detail response is identical to UpcomingEventResponse, consider combining if needed?
public record EventDetailResponse(string Name, DateOnly Date, CalendarEventType EventType, string ScheduledByUserName);
using Domain.CalendarEvents.Entities;

namespace Api.CalendarEvents.DTOs;

public record UpdateEventPollRequest(
    PollType PollType,
    DateTime ClosesAt,
    PollOptionsRequest[] Options);
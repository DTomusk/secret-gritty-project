using Domain.CalendarEvents.Entities;

namespace Api.CalendarEvents.DTOs;

public record CreateEventPollRequest(
    PollType PollType,
    DateTime ClosesAt);
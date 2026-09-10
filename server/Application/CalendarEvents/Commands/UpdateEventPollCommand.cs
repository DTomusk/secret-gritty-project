using Application.CalendarEvents.DTOs;
using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Commands;

public record UpdateEventPollCommand(
    Guid PollId,
    Guid EventId,
    Guid UserId,
    PollType PollType,
    DateTime ClosesAt,
    EventPollOptionsRequest[] Options);
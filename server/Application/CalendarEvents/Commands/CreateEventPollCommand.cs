using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.Commands;

public record CreateEventPollCommand(Guid EventId, Guid UserId, PollType PollType, DateTime ClosesAt);
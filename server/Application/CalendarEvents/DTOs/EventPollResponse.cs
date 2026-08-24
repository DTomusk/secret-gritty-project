using Domain.CalendarEvents.Entities;

namespace Application.CalendarEvents.DTOs;

public record EventPollResponse(Guid PollId, PollType Type, DateTime ClosesAt, IEnumerable<EventPollOptionResponse> Options);

// TODO: add users' votes later
public record EventPollOptionResponse(Guid OptionId, string OptionText);
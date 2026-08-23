using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.Shared.Interfaces;
using Domain.CalendarEvents.Entities;
using Domain.Shared.Results;

namespace Application.CalendarEvents.Handlers;

public class CreateEventPollCommandHandler : ICommandHandler<CreateEventPollCommand, CreateEventPollResponse>
{
    private readonly ICalendarEventRepository _calendarEventRepository;
    private readonly IEventPollRepository _eventPollRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventPollCommandHandler(ICalendarEventRepository calendarEventRepository, IEventPollRepository eventPollRepository, IUnitOfWork unitOfWork)
    {
        _calendarEventRepository = calendarEventRepository;
        _eventPollRepository = eventPollRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateEventPollResponse>> HandleAsync(CreateEventPollCommand command, CancellationToken cancellationToken = default)
    {
        // Get event and check that the user is the host
        var calendarEvent = await _calendarEventRepository.GetEventByIdAsync(command.EventId, cancellationToken);

        if (calendarEvent == null)
            return Result<CreateEventPollResponse>.Failure(new Error("Event with the specified ID not found.", ErrorType.NotFound));

        if (calendarEvent.HostUserId != command.UserId)
            return Result<CreateEventPollResponse>.Failure(new Error("User is not the host of the event.", ErrorType.Unauthorized));

        // Get polls for the event and check that there is no existing poll of the same type
        var polls = await _eventPollRepository.GetEventPollsByEventIdAsync(command.EventId, cancellationToken);

        if (polls.Any(p => p.Type == command.PollType))
            return Result<CreateEventPollResponse>.Failure(new Error("A poll of the specified type already exists for this event.", ErrorType.Conflict));

        // Create the poll and persist
        var poll = Poll.Create(calendarEvent, command.PollType, command.ClosesAt);
        await _eventPollRepository.CreateEventPollAsync(poll, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<CreateEventPollResponse>.Success(new CreateEventPollResponse(poll.Id));
    }
}

using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.CalendarEvents.Interfaces;
using Application.Shared.Interfaces;
using Domain.CalendarEvents.Entities;
using Domain.Shared.Results;

namespace Application.CalendarEvents.Handlers;

public class UpdateEventPollCommandHandler : ICommandHandler<UpdateEventPollCommand, UpdateEventPollResponse>
{
    private readonly ICalendarEventRepository _calendarEventRepository;
    private readonly IEventPollRepository _eventPollRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEventPollCommandHandler(ICalendarEventRepository calendarEventRepository, IEventPollRepository eventPollRepository, IUnitOfWork unitOfWork)
    {
        _calendarEventRepository = calendarEventRepository;
        _eventPollRepository = eventPollRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateEventPollResponse>> HandleAsync(UpdateEventPollCommand command, CancellationToken cancellationToken = default)
    {
        // Get event and check that the user is the host
        var calendarEvent = await _calendarEventRepository.GetEventByIdAsync(command.EventId, cancellationToken);

        if (calendarEvent == null)
            return Result<UpdateEventPollResponse>.Failure(new Error("Event with the specified ID not found.", ErrorType.NotFound));

        if (calendarEvent.HostUserId != command.UserId)
            return Result<UpdateEventPollResponse>.Failure(new Error("User is not the host of the event.", ErrorType.Unauthorized));

        // Get the poll, ensure it exists and matches the event and poll type
        var poll = await _eventPollRepository.GetEventPollByPollIdAsync(command.PollId, cancellationToken);

        // Obfuscate the error message to avoid revealing whether the poll exists or not
        if (poll == null || poll.EventId != command.EventId || poll.Type != command.PollType)
            return Result<UpdateEventPollResponse>.Failure(new Error("Poll with the specified ID not found.", ErrorType.NotFound));

        // Then delete all existing options and add the new ones (could be optimised, but no reason to at the moment)
        poll.DeleteOptions();

        // TODO: this logic could live in Poll
        foreach (var option in command.Options)
        {
            switch (command.PollType)
            {
                case PollType.Location:
                    poll.AddLocationOption(option.Location!);
                    break;
                case PollType.Date:
                    poll.AddDateOption(option.Date!.Value);
                    break;
                case PollType.Book:
                    poll.AddBookOption(option.Title!, option.Author!);
                    break;
                default:
                    return Result<UpdateEventPollResponse>.Failure(new Error("Invalid poll type.", ErrorType.Validation));
            }
        }

        await _eventPollRepository.UpdateEventPollAsync(poll, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<UpdateEventPollResponse>.Success(new UpdateEventPollResponse());
    }
}

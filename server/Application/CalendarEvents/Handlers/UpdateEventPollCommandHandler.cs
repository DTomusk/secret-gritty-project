using Application.CalendarEvents.Commands;
using Application.CalendarEvents.DTOs;
using Application.Shared.Interfaces;
using Domain.Shared.Results;

namespace Application.CalendarEvents.Handlers;

public class UpdateEventPollCommandHandler : ICommandHandler<UpdateEventPollCommand, UpdateEventPollResponse>
{
    public Task<Result<UpdateEventPollResponse>> HandleAsync(UpdateEventPollCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

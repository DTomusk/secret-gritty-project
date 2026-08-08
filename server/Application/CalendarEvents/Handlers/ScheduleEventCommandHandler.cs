using Application.CalendarEvents.Commands;
using Application.CalendarEvents.Interfaces;
using Application.Shared.Interfaces;
using Domain.CalendarEvents.Entities;
using Domain.Shared.Results;

namespace Application.CalendarEvents.Handlers;

public class ScheduleEventCommandHandler : ICommandHandler<ScheduleEventCommand>
{
    private readonly ICalendarEventRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleEventCommandHandler(ICalendarEventRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(ScheduleEventCommand command, CancellationToken cancellationToken = default)
    {
        // User id determined from current user in controller
        // Name and date already checked in fluent validation
        var calendarEvent = CalendarEvent.Create(command.UserId, command.Name, command.Date, command.EventType);
        await _repo.CreateAsync(calendarEvent, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}

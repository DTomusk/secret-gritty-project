using Application.Auth.Interfaces;
using Application.CalendarEvents.Commands;
using Application.CalendarEvents.Interfaces;
using Application.Shared.Interfaces;
using Domain.CalendarEvents.Entities;
using Domain.Shared.Results;

namespace Application.CalendarEvents.Handlers;

public class ChooseNextHostCommandHandler : ICommandHandler<ChooseNextHostCommand>
{
    private readonly ICalendarEventRepository _calendarEventRepo;
    private readonly IUserRepository _userRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ChooseNextHostCommandHandler(ICalendarEventRepository calendarEventRepo, IUserRepository userRepo, IUnitOfWork unitOfWork)
    {
        _calendarEventRepo = calendarEventRepo;
        _userRepo = userRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(ChooseNextHostCommand command, CancellationToken cancellationToken = default)
    {
        // Check there is no scheduled book club event in the future
        var futureEvent = await _calendarEventRepo.GetFutureEventByType(CalendarEventType.BookClub, cancellationToken);
        if (futureEvent is not null)
            return Result.Failure(new Error("There is already a scheduled book club event in the future.", ErrorType.Validation));

        var unscheduledEvent = await _calendarEventRepo.GetUnscheduledEventByType(CalendarEventType.BookClub, cancellationToken);
        if (unscheduledEvent is not null)
            return Result.Failure(new Error("There is already an upcoming book club, but the date hasn't been decided", ErrorType.Validation));

        var user = await _userRepo.GetByIdAsync(command.HostId, cancellationToken);
        if (user is null)
            return Result.Failure(new Error("The specified user does not exist.", ErrorType.Validation));

        // TODO: consider what to do about names
        var bookClubEvent = CalendarEvent.CreateUnscheduledEvent(user.Id, "Book club", CalendarEventType.BookClub);

        await _calendarEventRepo.CreateAsync(bookClubEvent, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}

using Api.CalendarEvents.DTOs;
using FluentValidation;

namespace Api.CalendarEvents.Validators;

public class CreateEventPollRequestValidator : AbstractValidator<CreateEventPollRequest>
{
    public CreateEventPollRequestValidator()
    {
        RuleFor(x => x.PollType).IsInEnum().WithMessage("PollType must be a valid enum value.");
        RuleFor(x => x.ClosesAt).GreaterThan(DateTime.UtcNow).WithMessage("ClosesAt must be a future date and time.");
    }
}

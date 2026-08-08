using Api.CalendarEvents.DTOs;
using FluentValidation;

namespace Api.CalendarEvents.Validators;

public class ScheduleEventRequestValidator : AbstractValidator<ScheduleEventRequest>
{
    public ScheduleEventRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Event name is required.")
            .MaximumLength(100).WithMessage("Event name must not exceed 100 characters.");
        RuleFor(x => x.Date)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Event date must be in the future.");
        RuleFor(x => x.EventType)
            .IsInEnum().WithMessage("Invalid event type.");
    }
}

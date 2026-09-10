using Api.CalendarEvents.DTOs;
using Domain.CalendarEvents.Entities;
using FluentValidation;

namespace Api.CalendarEvents.Validators;

public class UpdateEventPollRequestValidator : AbstractValidator<UpdateEventPollRequest>
{
    public UpdateEventPollRequestValidator()
    {
        RuleFor(x => x.PollType).IsInEnum().WithMessage("PollType must be a valid enum value.");
        RuleFor(x => x.ClosesAt).GreaterThan(DateTime.UtcNow).WithMessage("ClosesAt must be a future date and time.");
        RuleFor(x => x.Options)
            .NotEmpty().WithMessage("At least one option is required")
            .Must((request, options) => ValidateOptionsForType(request.PollType, options))
            .WithMessage("Options do not match the poll type");
    }

    private bool ValidateOptionsForType(PollType type, PollOptionsRequest[] options)
    {
        return type switch
        {
            PollType.Date => options.All(o => o.Date.HasValue && o.Date.Value > DateTime.UtcNow),
            PollType.Location => options.All(o => !string.IsNullOrWhiteSpace(o.Location)),
            PollType.Book => options.All(o => !string.IsNullOrWhiteSpace(o.Title) && !string.IsNullOrWhiteSpace(o.Author)),
            _ => false
        };
    }
}

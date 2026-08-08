using Api.Auth.DTOs;
using FluentValidation;

namespace Api.Auth.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.RegistrationCode)
            .NotEmpty()
            .Length(12);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100);
    }
}

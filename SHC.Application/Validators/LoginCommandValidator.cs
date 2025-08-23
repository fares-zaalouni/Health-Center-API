using FluentValidation;
using SHC.Application.Commands;

namespace SHC.Application.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{8}$")
            .WithMessage("Phone number must be 8 digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Role)
            .Empty()
            .WithMessage("Role should not be provided during login.");



    }
}

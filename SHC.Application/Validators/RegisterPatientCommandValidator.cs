using FluentValidation;
using SHC.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Validators
{
    public class RegisterPatientCommandValidator : AbstractValidator<RegisterPatientCommand>
    {
        public RegisterPatientCommandValidator() 
        {
            RuleFor(x => x.Firstname)
            .NotEmpty().WithMessage("Firstname is required.")
            .MaximumLength(50);

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Lastname is required.")
                .MaximumLength(50);

            RuleFor(x => x.Cin)
                .NotEmpty().WithMessage("CIN is required.")
                .Length(8).WithMessage("CIN must be exactly 8 characters.");

            RuleFor(x => x.Dob)
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{8,15}$").WithMessage("Phone number must be between 8 and 15 digits, optional '+' at start.");

            RuleFor(x => x.BloodType)
                .IsInEnum().WithMessage("Invalid blood type.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than 0.");

            // Optional fields

            RuleFor(x => x.EmergencyContactName)
                .MaximumLength(100).WithMessage("Emergency contact name cannot exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactName));

            RuleFor(x => x.EmergencyContactPhone)
                .Matches(@"^\+?\d{8,15}$").WithMessage("Emergency contact phone is invalid.")
                .When(x => !string.IsNullOrWhiteSpace(x.EmergencyContactPhone));

        }
    }
}

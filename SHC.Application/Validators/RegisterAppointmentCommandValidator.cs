using FluentValidation;
using SHC.Application.Commands;
using SHC.Core.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Validators
{
    public class RegisterAppointmentCommandValidator : AbstractValidator<RegisterAppointmentCommand>
    {
        public RegisterAppointmentCommandValidator() 
        {
            RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .WithMessage("Appointment date is required.")
            .GreaterThan(DateTime.Now)
            .WithMessage("Appointment date must be in the future.");

            RuleFor(x => x.Duration)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.AssignedDoctorId)
                .NotEmpty()
                .WithMessage("Assigned doctor ID is required.");

            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithMessage("Patient ID is required.");
        }
    }
}

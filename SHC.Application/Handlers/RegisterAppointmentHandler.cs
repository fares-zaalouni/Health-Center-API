using FluentValidation;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Handlers
{
    public class RegisterAppointmentHandler : IHandler<RegisterAppointmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterAppointmentCommand> _validator;
        private readonly IAppointmentService _appointmentService;

        public RegisterAppointmentHandler(
            IUnitOfWork unitOfWork, 
            IValidator<RegisterAppointmentCommand> validator, 
            IAppointmentService appointmentService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _appointmentService = appointmentService;
        }

        public async Task<Unit> Handle(RegisterAppointmentCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Validation failed:\n{errors}");
            }
            Patient? patient = await _unitOfWork.Patients.GetByIdAsync(command.PatientId);
            if (patient == null)
                throw new Exception("NOOO");
            return Unit.Value; 
        }
    }
}

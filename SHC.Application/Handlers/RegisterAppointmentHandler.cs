using FluentValidation;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.Exceptions;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SHC.Application.Handlers
{
    public class RegisterAppointmentHandler : IHandler<RegisterAppointmentCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientQueryRepository _patientQueryRepository;
        private readonly IValidator<RegisterAppointmentCommand> _validator;
        private readonly IAppointmentService _appointmentService;

        public RegisterAppointmentHandler(
            IUnitOfWork unitOfWork,
            IPatientQueryRepository patientQueryRepository,
            IValidator<RegisterAppointmentCommand> validator, 
            IAppointmentService appointmentService)
        {
            _unitOfWork = unitOfWork;
            _patientQueryRepository = patientQueryRepository;
            _validator = validator;
            _appointmentService = appointmentService;
        }

        public async Task<Result<Unit>> Handle(RegisterAppointmentCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<Unit>.Failure($"Validation failed:\n{errors}");
            }
            Patient? patient = await _patientQueryRepository.GetByIdAsync(command.PatientId);
            if (patient == null)
                return Result<Unit>.Failure($"Patient with ID {command.PatientId} doesn't exist");

            Appointment appointment = new Appointment(
                Guid.NewGuid(),
                command.AppointmentDate
                //command.AssignedDoctorId
                );

            if (command.IsUrgent.HasValue) 
                appointment.SetUrgency(command.IsUrgent.Value);

            if(command.Duration.HasValue)
                appointment.SetDuration(command.Duration.Value);


            List<Appointment> overlapped = _appointmentService.ValidateAppointment(appointment, patient.Appointments);
            if( overlapped.Count > 0)
            {
                string errorMessage = "Appointment overlaps with existing appointments:\n" +
                    string.Join("\n", overlapped.Select(a => $"{a.AppointmentDate} - {a.AppointmentDate.AddMinutes(a.DurationInMin)}"));
                return Result<Unit>.Failure(errorMessage);
            }

            patient.Appointments.Add(appointment);
            await _unitOfWork.SaveAsync();
            return Result<Unit>.Success(Unit.Value); 
        }
    }
}

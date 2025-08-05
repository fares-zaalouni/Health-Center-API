using FluentValidation;
using SHC.Application.Commands;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Handlers
{
    public class RegisterPatientHandler : IHandler<RegisterPatientCommand, Patient>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterPatientCommand> _validator;
        public RegisterPatientHandler(IUnitOfWork unitOfWork, IValidator<RegisterPatientCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Patient> Handle(RegisterPatientCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Validation failed:\n{errors}");
            }
            User user = new User(Guid.NewGuid(), command.Firstname, command.Lastname, command.Cin, command.Dob,command.Email, command.PhoneNumber);
            Patient patient = new Patient(Guid.NewGuid(), user.Id, command.EmergencyContactName, command.EmergencyContactPhone, command.BloodType, command.Weight, command.Height);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveAsync();
            return patient;
        }
    }
}

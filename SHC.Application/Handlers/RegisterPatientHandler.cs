using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.Handlers
{
    public class RegisterPatientHandler : IHandler<RegisterPatientCommand, Result<Patient>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IValidator<RegisterPatientCommand> _validator;
        private readonly IUserService _userService;
        public RegisterPatientHandler(
            IUnitOfWork unitOfWork,
            IUserQueryRepository userQueryRepository,
            IValidator<RegisterPatientCommand> validator,
            IUserService userService
            )
        {
            _unitOfWork = unitOfWork;
            _userQueryRepository = userQueryRepository;
            _validator = validator;
            _userService = userService;
        }

        public async Task<Result<Patient>> Handle(RegisterPatientCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Validation failed:\n{errors}");
            }
             
            bool unique = await _userService.IsUserUnique(command.PhoneNumber);
            Guid userId = Guid.Empty;
            if (!unique)
            {

                User? user = await _userQueryRepository.GetByPhoneNumberAsync(command.PhoneNumber);
                if( user == null)
                    throw new Exception($"User with phone number {command.PhoneNumber} was not returned from dbcontext.");
                userId = user!.Id;
            } else
            {
                User user = new User(
                    Guid.NewGuid(),
                    _userService.HashPassword(command.Password),
                    command.PhoneNumber,
                    [Roles.Patient]
                    );
                await _unitOfWork.Users.AddAsync(user);
                userId = user.Id;
            }

            Patient patient = new Patient(
                    Guid.NewGuid(),
                    userId,
                    command.Firstname,
                    command.Lastname,
                    command.Dob,
                    command.Cin,
                    command.Email,
                    command.EmergencyContactName,
                    command.EmergencyContactPhone,
                    command.BloodType,
                    command.Weight,
                    command.Height);

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveAsync();
            return Result<Patient>.Success(patient);
        }
    }
}

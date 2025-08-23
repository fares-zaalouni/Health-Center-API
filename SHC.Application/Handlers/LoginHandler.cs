using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.DTOs;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;
using SHC.Core.Projections;

namespace SHC.Application.Handlers;

public class LoginHandler : IHandler<LoginCommand, Result<LoginResponseDTO>>
{
    private readonly IUserService _userService;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPatientQueryRepository _patientQueryRepository;

    public LoginHandler(
        IUserService userService,
        IUserQueryRepository userQueryRepository,
        IPatientQueryRepository patientQueryRepository = null
        )
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _patientQueryRepository = patientQueryRepository ?? throw new ArgumentNullException(nameof(patientQueryRepository));
    }
    public async Task<Result<LoginResponseDTO>> Handle(LoginCommand command)
    {
        bool isValid =  await _userService.IsPasswordValidAsync(command.PhoneNumber, command.Password);
        if (!isValid)
        {
            return Result<LoginResponseDTO>.Failure("Invalid phone number or password.");
        }
        FullName? userInfo = await _patientQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(command.PhoneNumber);
        if (userInfo == null)
        {
            return Result<LoginResponseDTO>.Failure("User not found.");
        }
        return Result<LoginResponseDTO>.Success(new LoginResponseDTO(userInfo?.FirstName!, userInfo?.LastName!, "", ""));
    }
}

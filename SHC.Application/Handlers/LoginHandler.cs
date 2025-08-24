using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.DTOs;
using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;
using SHC.Core.Projections;
using SHC.Infrastructure.Models;

namespace SHC.Application.Handlers;

public class LoginHandler : IHandler<LoginCommand, Result<LoginResponseDTO>>
{
    private readonly IUserService _userService;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IPatientQueryRepository _patientQueryRepository;
    private readonly IDoctorQueryRepository _doctorQueryRepository;
    private readonly IAuthService _authService;

    public LoginHandler(
        IUserService userService,
        IUserQueryRepository userQueryRepository,
        IPatientQueryRepository patientQueryRepository,
        IDoctorQueryRepository doctorQueryRepository,
        IAuthService authService
        )
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _patientQueryRepository = patientQueryRepository ?? throw new ArgumentNullException(nameof(patientQueryRepository));
        _doctorQueryRepository = doctorQueryRepository ?? throw new ArgumentNullException(nameof(doctorQueryRepository));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    public async Task<Result<LoginResponseDTO>> Handle(LoginCommand command)
    {
        bool isValid =  await _userService.IsPasswordValidAsync(command.PhoneNumber, command.Password);
        if (!isValid)
        {
            return Result<LoginResponseDTO>.Failure("Invalid phone number or password.");
        }
        if(_userQueryRepository.HasRoleByPhoneNumber(command.PhoneNumber, command.Role) == false)
        {
            return Result<LoginResponseDTO>.Failure("Forbidden");
        }

        FullName? userInfo = null;
        if(command.Role == Roles.Patient)
            userInfo = await _patientQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(command.PhoneNumber);
        if(command.Role == Roles.Doctor)
            userInfo = await _doctorQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(command.PhoneNumber);

        if (userInfo == null)
        {
            throw new Exception("User info should not be null here.");
        }

        RefreshToken refreshToken;
        SecurityToken token;
        Guid? userId = await _userQueryRepository.GetIdByPhoneNumber(command.PhoneNumber);
        if (userId == null)
        {
            throw new Exception("User ID should not be null here.");
        }
        _authService.GenerateLoginTokens((Guid)userId, command.PhoneNumber, Guid.NewGuid(), command.Role,  out token,  out refreshToken);
        _authService.SaveRefreshToken(refreshToken);
        RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO
        {
            Id = refreshToken.Id,
            Created = refreshToken.Created,
            Expires = refreshToken.Expires,
            Token = refreshToken.Token
        };
        return Result<LoginResponseDTO>.Success(new LoginResponseDTO(userInfo?.FirstName!, userInfo?.LastName!, token, refreshTokenDTO));
    }
}

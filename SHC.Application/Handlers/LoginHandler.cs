using FluentValidation;
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
    private readonly ISecretaryQueryRepository _secretaryQueryRepository;
    private readonly IAuthService _authService;
    private readonly IValidator<LoginCommand> _validator;



    // TO DO
    // Make more specific Exceptions and Handle them in the Middleware
    public LoginHandler(
        IUserService userService,
        IUserQueryRepository userQueryRepository,
        IPatientQueryRepository patientQueryRepository,
        IDoctorQueryRepository doctorQueryRepository,
        ISecretaryQueryRepository secretaryQueryRepository,
        IAuthService authService,
        IValidator<LoginCommand> validator
        )
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
        _patientQueryRepository = patientQueryRepository ?? throw new ArgumentNullException(nameof(patientQueryRepository));
        _doctorQueryRepository = doctorQueryRepository ?? throw new ArgumentNullException(nameof(doctorQueryRepository));
        _secretaryQueryRepository = secretaryQueryRepository ?? throw new ArgumentNullException(nameof(_secretaryQueryRepository));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }
    public async Task<Result<LoginResponseDTO>> Handle(LoginCommand command)
    {
        var result = await _validator.ValidateAsync(command);
        if(result.IsValid == false)
        {
            var errors = string.Join("\n", result.Errors.Select(e => e.ErrorMessage));
            return Result<LoginResponseDTO>.Failure($"Validation failed:\n{errors}");
        }

        bool isValid =  await _userService.IsPasswordValidAsync(command.PhoneNumber, command.Password);
        if (!isValid)
        {
            return Result<LoginResponseDTO>.Failure("Invalid phone number or password.");
        }

        if(_userQueryRepository.HasRoleByPhoneNumber(command.PhoneNumber, command.Role) == false)
        {
            return Result<LoginResponseDTO>.Failure("Forbidden");
        }

        FullName? userInfo = await GetUserInfoByRoleAndPhoneNumber(command.Role, command.PhoneNumber);
        
        Guid? userId = await _userQueryRepository.GetIdByPhoneNumberAsync(command.PhoneNumber);
        if (userId == null)
        {
            throw new Exception("User ID should not be null here.");
        }
        var tokens = _authService.GenerateLoginTokens((Guid)userId, command.PhoneNumber, command.DeviceId, command.Role);
        await _authService.SaveRefreshToken(tokens.RefreshToken);
        RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO
        {
            Id = tokens.RefreshToken.Id,
            Expires = tokens.RefreshToken.Expires,
            Token = tokens.RefreshToken.Token
        };
        AccessTokenDTO accessTokenDTO = new AccessTokenDTO
        {
            Expires = tokens.AccessToken.Expires,
            Token = tokens.AccessToken.Token
        };
        return Result<LoginResponseDTO>.Success(new LoginResponseDTO(userInfo?.FirstName!, userInfo?.LastName!, accessTokenDTO, refreshTokenDTO));
    }

    private Task<FullName?> GetUserInfoByRoleAndPhoneNumber(Roles role, string phoneNumber)
    {
        return role switch
        {
            Roles.Patient => _patientQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(phoneNumber),
            Roles.Doctor => _doctorQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(phoneNumber),
            Roles.Secretary => _secretaryQueryRepository.GetFirstAndLastNameByPhoneNumberAsync(phoneNumber),
            _ => throw new Exception("Role not supported."),
        };
    }
}

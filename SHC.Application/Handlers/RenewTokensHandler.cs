using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.DTOs;
using SHC.Core.Interfaces;
using SHC.Infrastructure.Models;
using System.Reflection.Metadata.Ecma335;

namespace SHC.Application.Handlers;

public class RenewTokensHandler : IHandler<RenewTokensCommand, Result<RenewTokensResponseDTO>>
{
    private readonly IAuthService _authService;
    public RenewTokensHandler(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    public async Task<Result<RenewTokensResponseDTO>> Handle(RenewTokensCommand command)
    {
        var tokens = await _authService.RenewTokens(command.RefreshToken, command.UserId, command.PhoneNumber, command.DeviceId);
        if (tokens == null)
        {
            return Result<RenewTokensResponseDTO>.Failure("Unauthorized");
        }
        AccessTokenDTO accessTokenDTO = new AccessTokenDTO
        {
            Token = tokens.AccessToken.Token,
            ExpiresAt = tokens.AccessToken.ExpiresAt
        };
        RefreshTokenDTO refreshTokenDTO = new RefreshTokenDTO
        {
            Token = tokens.RefreshToken.Token,
            ExpiresAt = tokens.RefreshToken.ExpiresAt
        };
        var response = new RenewTokensResponseDTO
        {
            AccessToken = accessTokenDTO,
            RefreshToken = refreshTokenDTO
        };
        return Result<RenewTokensResponseDTO>.Success(response);
    }
}

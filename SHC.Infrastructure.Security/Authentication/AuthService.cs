using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Infrastructure.Models;
using SHC.Infrastructure.Security.JWT;


namespace SHC.Infrastructure.Security.Authentication;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork
        )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }
    public Tokens GenerateLoginTokens(Guid userId, string phoneNumber, Guid deviceId, Roles role)
    {
        AccessToken token = _jwtTokenGenerator.GenerateToken(userId, phoneNumber, role);
        RefreshToken refreshToken = _jwtTokenGenerator.GenerateRefreshToken(userId, deviceId, role);
        return new Tokens
        {
            AccessToken = token,
            RefreshToken = refreshToken
        };
    }
    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveAsync();
    }

    // NEEDS TO TAKE CARE OF REVOKED TOKENS
    public async Task<RefreshToken?> ValidateRefreshToken(string token, Guid userId, Guid deviceId)
    {
        RefreshToken? refreshToken = await _refreshTokenRepository.GetByTokenAndDeviceId(token, deviceId);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
            return null;
        if (refreshToken.Revoked.HasValue)
        {
            // Log token reuse attempt 
            return null;
        }
        if (refreshToken.UserId != userId)
        {
            // Log token user mismatch  
            return null;
        }
        return refreshToken;
    }

    public async Task RevokeRefreshToken(string refreshToken, Guid deviceId, Guid? replacedByToken = null)
    {
        RefreshToken? token = await _refreshTokenRepository.GetByTokenAndDeviceId(refreshToken, deviceId);
        if (token == null)
            throw new Exception("Token not found");

        token.Revoked = DateTime.UtcNow;
        token.ReplacedByToken = replacedByToken;
        await _unitOfWork.SaveAsync();
    }

    public async Task<Tokens?> RenewTokens(string refreshToken, Guid userId, string phoneNumber, Guid deviceId)
    {
        RefreshToken? token = await ValidateRefreshToken(refreshToken, userId, deviceId);
        if (token == null)
            return null;
        Tokens newTokens = GenerateLoginTokens(userId, phoneNumber, deviceId, token.Role);
        await SaveRefreshToken(newTokens.RefreshToken);
        await RevokeRefreshToken(refreshToken, deviceId, newTokens.RefreshToken.Id);
        return newTokens;
    }
}

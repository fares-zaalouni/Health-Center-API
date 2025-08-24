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
    public void GenerateLoginTokens(Guid userId, string phoneNumber, Guid deviceId, Roles role, out SecurityToken token, out RefreshToken refreshToken)
    {
        token = _jwtTokenGenerator.GenerateToken(userId, phoneNumber, role);
        refreshToken = _jwtTokenGenerator.GenerateRefreshToken(userId, deviceId, role);
    }
    public void SaveRefreshToken(RefreshToken refreshToken)
    {
        _refreshTokenRepository.AddAsync(refreshToken);
        _unitOfWork.SaveAsync();
    }
}

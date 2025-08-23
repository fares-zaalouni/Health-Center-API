using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using SHC.Infrastructure.Models;
using SHC.Infrastructure.Security.JWT;


namespace SHC.Infrastructure.Security.Authentication;

public class AuthService
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
    public void Authenticate(User user, Guid deviceId, Roles role, out SecurityToken token, out RefreshToken refreshToken)
    {
        token = _jwtTokenGenerator.GenerateToken(user, role);
        RefreshToken newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken(user.Id, deviceId, role);
        _refreshTokenRepository.AddAsync(newRefreshToken);
        _unitOfWork.SaveAsync();
        refreshToken = newRefreshToken;
    }
}

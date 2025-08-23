namespace SHC.Infrastructure.Models;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
}

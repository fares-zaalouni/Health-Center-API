namespace SHC.Infrastructure.Models;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAndDeviceId(string token, Guid deviceId);
    Task<RefreshToken?> GetByIdAsync(Guid id);
}

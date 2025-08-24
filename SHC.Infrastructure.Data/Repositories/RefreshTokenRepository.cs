
using Microsoft.EntityFrameworkCore;
using SHC.Infrastructure.Models;

namespace SHC.Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly SHCContext _context;
    public RefreshTokenRepository(SHCContext context)
    {
        _context = context;
    }
    public  Task AddAsync(RefreshToken refreshToken)
    {
        return _context.DBRefreshToken.AddAsync(refreshToken).AsTask();
    }

    public Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return _context.DBRefreshToken
            .Where(rf => rf.Id == id)
            .FirstOrDefaultAsync();
    }

    public Task<RefreshToken?> GetByTokenAndDeviceId(string token, Guid deviceId)
    {
        return _context.DBRefreshToken
            .Where(rf => rf.Token == token && rf.DeviceId == deviceId)
            .FirstOrDefaultAsync();
    }
}

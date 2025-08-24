
using SHC.Infrastructure.Models;

namespace SHC.Infrastructure.Data.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly SHCContext _context;
    public RefreshTokenRepository(SHCContext context)
    {
        _context = context;
    }
    public  async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.DBRefreshToken.AddAsync(refreshToken).AsTask();
    }
}

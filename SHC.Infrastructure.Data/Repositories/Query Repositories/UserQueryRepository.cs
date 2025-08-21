using Microsoft.EntityFrameworkCore;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces.IRepositories;

namespace SHC.Infrastructure.Data.Repositories.Query_Repositories;

public class UserQueryRepository : IUserQueryRepository
{
    private readonly SHCContext _dbContext;
    public UserQueryRepository(SHCContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<List<User>> GetAllAsync()
    {
        return _dbContext.DBUser.ToListAsync();
    }

    public async Task<bool> IsUserUniqueAsync(string phoneNumber)
    {
        return !await _dbContext.DBUser.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return _dbContext.DBUser.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public IQueryable<User> Query()
    {
        return _dbContext.DBUser.AsQueryable();
    }
}

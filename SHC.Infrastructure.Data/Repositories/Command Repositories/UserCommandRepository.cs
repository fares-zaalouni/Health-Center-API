using SHC.Core.Domain.User;
using SHC.Core.Interfaces.IRepositories;


namespace SHC.Infrastructure.Data.Repositories.Command_Repositories;

public class UserCommandRepository : IUserCommandRepository
{
    private readonly SHCContext _dbContext;
    public UserCommandRepository(SHCContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.DBUser.AddAsync(user);
    }

    public async Task DeleteAsync(Guid patientId)
    {
        throw new NotImplementedException();
    }
}

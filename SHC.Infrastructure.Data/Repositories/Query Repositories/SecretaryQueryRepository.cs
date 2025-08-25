
using Microsoft.EntityFrameworkCore;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Projections;

namespace SHC.Infrastructure.Data.Repositories.Query_Repositories;

public class SecretaryQueryRepository : ISecretaryQueryRepository
{
    private readonly SHCContext _dbContext;
    public SecretaryQueryRepository(SHCContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<FullName?> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber)
    {
        return (
                    from u in _dbContext.DBUser
                    join s in _dbContext.DBSecretary on u.Id equals s.UserId
                    where u.PhoneNumber == phoneNumber
                    select new FullName(s.Firstname, s.Lastname)
                ).FirstOrDefaultAsync();
    }
}

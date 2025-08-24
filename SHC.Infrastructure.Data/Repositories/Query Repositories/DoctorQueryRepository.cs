
using Microsoft.EntityFrameworkCore;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Projections;

namespace SHC.Infrastructure.Data.Repositories.Query_Repositories;

public class DoctorQueryRepository : IDoctorQueryRepository
{
    private readonly SHCContext _dbContext;
    public DoctorQueryRepository(SHCContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<FullName?> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber)
    {
        return (
                    from u in _dbContext.DBUser
                    join d in _dbContext.DBDoctor on u.Id equals d.UserId
                    where u.PhoneNumber == phoneNumber
                    select new FullName(d.Firstname, d.Lastname)
                ).FirstOrDefaultAsync();
    }
}

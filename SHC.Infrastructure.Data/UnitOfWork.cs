using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using SHC.Core.Interfaces.IRepositories;
using SHC.Infrastructure.Data.Repositories;
using SHC.Infrastructure.Data.Repositories.Command_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SHCContext _dbContext;
    public IPatientCommandRepository Patients { get; }
    public IUserCommandRepository Users { get; }
    public UnitOfWork(SHCContext dbContext)
    {
        _dbContext = dbContext;
        Patients = new PatientCommandRepository(_dbContext);
        Users = new UserCommandRepository(_dbContext);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}

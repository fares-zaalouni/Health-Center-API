using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using SHC.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SHCContext _dbContext;
        public IPatientRepository Patients { get; }
        public IUserRepository Users { get; }
        public UnitOfWork(SHCContext dbContext)
        {
            _dbContext = dbContext;
            Patients = new PatientRepository(_dbContext);
            Users = new UserRepository(_dbContext);
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
}

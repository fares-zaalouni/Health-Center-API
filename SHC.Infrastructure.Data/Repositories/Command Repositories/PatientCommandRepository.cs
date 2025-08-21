using Microsoft.EntityFrameworkCore;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data.Repositories.Command_Repositories
{
    public class PatientCommandRepository : IPatientCommandRepository
    {
        private readonly SHCContext _dbContext;
        public PatientCommandRepository(SHCContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<Patient> AddAsync(Patient patient)
        {
            await _dbContext.DBPatient.AddAsync(patient);
            return patient;
        }

        public Task DeleteAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }
    }
}

using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly SHCContext _dbContext;
        public PatientRepository(SHCContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<Patient?> GetByIdAsync(Guid patientId)
        {
           return await _dbContext.DBPatient.FindAsync(patientId);        
        }
    }
}

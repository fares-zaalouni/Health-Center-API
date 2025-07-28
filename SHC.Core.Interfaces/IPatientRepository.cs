using SHC.Core.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(Guid patientId);
        Task<Patient> AddAsync(Patient patient);              
        Task DeleteAsync(Guid patientId);

    }
}

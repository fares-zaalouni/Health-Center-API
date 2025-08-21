using SHC.Core.Domain.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IRepositories;

public interface IPatientCommandRepository
{
    Task<Patient> AddAsync(Patient patient);
    Task DeleteAsync(Guid patientId);
}

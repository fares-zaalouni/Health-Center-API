using SHC.Core.Domain.Patient;
using SHC.Core.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IRepositories;

public interface IPatientQueryRepository
{
    Task<Patient?> GetByIdAsync(Guid patientId);
    IQueryable<Patient> Query();
    Task<FullName?> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber);
}

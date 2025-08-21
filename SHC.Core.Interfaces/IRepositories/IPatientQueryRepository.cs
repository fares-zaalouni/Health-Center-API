using SHC.Core.Domain.Patient;
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
    Task<(string firstname, string lastname)> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber);
}

using Microsoft.EntityFrameworkCore;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data.Repositories.Query_Repositories;

public class PatientQueryRepository : IPatientQueryRepository
{
    private readonly SHCContext _dbContext;

    public PatientQueryRepository(SHCContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public Task<Patient?> GetByIdAsync(Guid patientId)
    {
        return _dbContext.DBPatient.FindAsync(patientId).AsTask();
    }
    public Task<(string firstname, string lastname)> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber)
    {
        return (
            from u in _dbContext.DBUser
            join p in _dbContext.DBPatient on u.Id equals p.UserId
            where u.PhoneNumber == phoneNumber
            select new ValueTuple<string, string>(p.Firstname, p.Lastname)
        ).FirstOrDefaultAsync();
    }
    public IQueryable<Patient> Query()
    {
        return _dbContext.DBPatient.AsQueryable();
    }
}

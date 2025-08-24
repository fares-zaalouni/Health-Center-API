using SHC.Core.Projections;

namespace SHC.Core.Interfaces.IRepositories;

public interface IDoctorQueryRepository
{
    Task<FullName?> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber);

}

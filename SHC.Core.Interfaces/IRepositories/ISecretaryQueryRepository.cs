
using SHC.Core.Projections;

namespace SHC.Core.Interfaces.IRepositories;

public interface ISecretaryQueryRepository
{
    Task<FullName?> GetFirstAndLastNameByPhoneNumberAsync(string phoneNumber);
}

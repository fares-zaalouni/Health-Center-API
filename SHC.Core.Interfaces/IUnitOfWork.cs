using SHC.Core.Interfaces.IRepositories;

namespace SHC.Core.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IPatientCommandRepository Patients { get; }
    IUserCommandRepository Users { get; }
    Task SaveAsync(CancellationToken cancellationToken = default);
}

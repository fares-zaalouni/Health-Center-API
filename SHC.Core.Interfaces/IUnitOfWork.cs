using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IPatientRepository Patients { get; }
        IUserRepository Users { get; }
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}

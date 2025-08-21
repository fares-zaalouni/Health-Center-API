using SHC.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IRepositories
{
    public interface IUserCommandRepository
    {
        Task AddAsync(User user);
        Task DeleteAsync(Guid patientId);
    }
}

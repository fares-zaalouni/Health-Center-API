using SHC.Core.Domain.Patient;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SHCContext _dbContext;
        public UserRepository(SHCContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await _dbContext.DBUser.AddAsync(user);
            return user;
        }

        public async Task DeleteAsync(Guid patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}

using SHC.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IRepositories;

public interface IUserQueryRepository
{
    Task<List<User>> GetAllAsync();

    Task<bool> IsUserUniqueAsync(string phoneNumber);

    Task<User?> GetByIdAsync(Guid userId);


    Task<User?> GetByPhoneNumberAsync(string phoneNumber);


    public IQueryable<User> Query();
}

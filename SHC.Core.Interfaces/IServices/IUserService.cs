using SHC.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Core.Interfaces.IServices;

public interface IUserService
{
    Task<bool> IsUserUnique(string phoneNumber);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
    Task<bool> IsPasswordValidAsync(string phone, string password);
}

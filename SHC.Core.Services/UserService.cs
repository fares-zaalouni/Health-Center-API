using Microsoft.AspNetCore.Identity;
using SHC.Core.Domain.User;
using SHC.Core.Interfaces.IRepositories;
using SHC.Core.Interfaces.IServices;


namespace SHC.Core.Services;

public class UserService : IUserService
{
    private readonly IUserQueryRepository _userRepository;
    private readonly PasswordHasher<string> _hasher = new();

    public UserService(IUserQueryRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(null, password);
    }

    public Task<bool> IsUserUnique(string phoneNumber)
    {
        return _userRepository.IsUserUniqueAsync(phoneNumber);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return _hasher.VerifyHashedPassword(null, hashedPassword, password) == PasswordVerificationResult.Success;
    }

    public async Task<bool> IsPasswordValidAsync(string phone, string password)
    {
        var user = await _userRepository.GetByPhoneNumberAsync(phone);
        if (user == null)
        {
            return false;
        }
        return VerifyPassword(password, user.HashedPassword);
    }

    
}

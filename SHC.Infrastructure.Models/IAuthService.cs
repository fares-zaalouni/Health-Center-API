
using SHC.Core.Domain.User;

namespace SHC.Infrastructure.Models;

public interface IAuthService
{
    Tokens GenerateLoginTokens(Guid userId, string phoneNumber, Guid deviceId, Roles role);
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> ValidateRefreshToken(string token, Guid userId, Guid deviceId);
    Task RevokeRefreshToken(string refreshToken, Guid deviceId, Guid? replacedByToken = null);
    Task<Tokens?> RenewTokens(string refreshToken, Guid userId, string phoneNumber, Guid deviceId);


}

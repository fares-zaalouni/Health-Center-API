
using SHC.Core.Domain.User;

namespace SHC.Infrastructure.Models;

public interface IAuthService
{
    void GenerateLoginTokens(Guid userId, string phoneNumber, Guid deviceId, Roles role, out SecurityToken token, out RefreshToken refreshToken);
    void SaveRefreshToken(RefreshToken refreshToken);

}

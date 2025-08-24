using SHC.Core.Domain.User;
using SHC.Infrastructure.Models;


namespace SHC.Infrastructure.Security.JWT;

public interface IJwtTokenGenerator
{
    AccessToken GenerateToken(Guid userId, string phoneNumber, Roles role);
    RefreshToken GenerateRefreshToken(Guid userId, Guid deviceID, Roles role);

}

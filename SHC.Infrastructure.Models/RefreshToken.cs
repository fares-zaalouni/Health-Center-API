
using SHC.Core.Domain.User;

namespace SHC.Infrastructure.Models;

public record RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public Guid? ReplacedByToken { get; set; }
    public DateTime? Revoked { get; set; }
    public Guid UserId { get; set; }
    public Roles Role { get; set; } = default!;
    public Guid DeviceId { get; set; }
}

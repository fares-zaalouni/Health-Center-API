
using SHC.Infrastructure.Models;

namespace SHC.Application.DTOs;

public class RefreshTokenDTO
{
    public Guid Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
}

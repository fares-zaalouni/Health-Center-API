
using SHC.Infrastructure.Models;

namespace SHC.Application.DTOs;

public record RefreshTokenDTO
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}

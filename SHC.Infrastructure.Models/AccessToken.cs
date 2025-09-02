
namespace SHC.Infrastructure.Models;

public record AccessToken
{
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

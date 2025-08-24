
namespace SHC.Infrastructure.Models;

public record Tokens
{
    public AccessToken AccessToken { get; init; }
    public RefreshToken RefreshToken { get; init; }
}

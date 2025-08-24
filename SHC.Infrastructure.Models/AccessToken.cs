
namespace SHC.Infrastructure.Models;

public record AccessToken
{
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
}

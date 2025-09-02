
namespace SHC.Application.DTOs
{
    public record AccessTokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}

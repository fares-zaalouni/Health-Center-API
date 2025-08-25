
namespace SHC.Application.DTOs
{
    public record AccessTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}

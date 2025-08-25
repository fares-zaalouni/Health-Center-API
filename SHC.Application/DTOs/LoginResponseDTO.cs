using SHC.Infrastructure.Models;
using SHC.Infrastructure.Security.JWT;

namespace SHC.Application.DTOs
{
    public record LoginResponseDTO
    {
        public AccessTokenDTO AccessToken { get;}
        public RefreshTokenDTO RefreshTokenDTO { get; }
        public string Firstname { get; set; }  
        public string Lastname { get; set; }
        public LoginResponseDTO(string firstName, string lastName, AccessTokenDTO accessToken, RefreshTokenDTO refreshToken)
        {
            Firstname = firstName;
            Lastname = lastName;
            AccessToken = accessToken;
            RefreshTokenDTO = refreshToken;
        }
    }
}

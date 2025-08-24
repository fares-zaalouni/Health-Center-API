using SHC.Infrastructure.Models;
using SHC.Infrastructure.Security.JWT;

namespace SHC.Application.DTOs
{
    public class LoginResponseDTO
    {
        public SecurityToken Token { get;}
        public RefreshTokenDTO RefreshTokenDTO { get; }
        public string Firstname { get; set; }  
        public string Lastname { get; set; }
        public LoginResponseDTO(string firstName, string lastName, SecurityToken token, RefreshTokenDTO refreshToken)
        {
            Firstname = firstName;
            Lastname = lastName;
            Token = token;
            RefreshTokenDTO = refreshToken;
        }
    }
}

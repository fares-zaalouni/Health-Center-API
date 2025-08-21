using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get;}
        public string RefreshToken { get; }
        public string Firstname { get; set; }  
        public string Lastname { get; set; }
        public LoginResponseDTO(string firstName, string lastName, string token, string refreshToken)
        {
            Firstname = firstName;
            Lastname = lastName;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}

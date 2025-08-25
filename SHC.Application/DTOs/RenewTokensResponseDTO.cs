using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHC.Application.DTOs;

public record RenewTokensResponseDTO
{
    public AccessTokenDTO AccessToken { get; set; }
    public RefreshTokenDTO RefreshToken { get; set; }
}

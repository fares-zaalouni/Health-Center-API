
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SHC.Core.Domain.User;
using SHC.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SHC.Infrastructure.Security.JWT;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    public JwtTokenGenerator(IOptions<JwtOptions> options)
        => _options = options.Value;

    public Models.AccessToken GenerateToken(Guid userId, string phoneNumber, Roles role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.MobilePhone, phoneNumber),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString())
        };
        Console.WriteLine(_options.Key);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenExpirationMinutes),
            signingCredentials: creds);

        return new Models.AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Created = DateTime.UtcNow,
            Expires = token.ValidTo
        };
    }

    public RefreshToken GenerateRefreshToken(Guid userId, Guid deviceID, Roles role)
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(randomNumber),
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(_options.RefreshTokenExpirationDays),
            Role = role,
            UserId = userId,
            DeviceId = deviceID
        };
    }
}

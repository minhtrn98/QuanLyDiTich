using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLDT.Application.Security.Token;
using QLDT.Domain.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLDT.Infrastructure.Security.Token;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeService _dateTimeService;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeService dateTimeService, IOptions<JwtSettings> jwtSettingsOptions)
    {
        _dateTimeService = dateTimeService;
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        JwtSecurityToken securityToken = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeService.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}

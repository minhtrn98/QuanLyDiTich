using System.ComponentModel.DataAnnotations;

namespace QLDT.Infrastructure.Security.Token;

public sealed record JwtSettings
{
    public const string NAME = "JwtSettings";
    [Required] public string Secret { get; init; } = string.Empty;
    [Required] public int ExpiryMinutes { get; init; }
    [Required] public string Issuer { get; init; } = string.Empty;
    [Required] public string Audience { get; init; } = string.Empty;
}

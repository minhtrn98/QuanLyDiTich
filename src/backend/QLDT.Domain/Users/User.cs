namespace QLDT.Domain.Users;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime? Dob { get; set; }
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = default!;
    public bool IsEmailVerified { get; set; }
}

public sealed class UserSchema
{
    public const int FIRST_NAME_MAX_LENGTH = 255;
    public const int LAST_NAME_MAX_LENGTH = 255;
}

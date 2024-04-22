namespace QLDT.Domain.Users;

public sealed class User : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime? Dob { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsEmailVerified { get; set; }
}

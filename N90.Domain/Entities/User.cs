using N90.Domain.Common.Entities;
using N90.Domain.Enums;

namespace N90.Domain.Entities;

public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public int Age { get; set; }

    public string EmailAddress { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public bool IsEmailAddressVerified { get; set; }

    public RoleType Role { get; set; }

    public UserSettings? UserSettings { get; set; }
}
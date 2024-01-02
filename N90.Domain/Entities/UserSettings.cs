using N90.Domain.Common.Entities;
using N90.Domain.Enums;

namespace N90.Domain.Entities;

public class UserSettings : IEntity
{
    public NotificationType? PreferredNotificationType { get; set; }

    /// <summary>
    ///     Gets or sets the user Id
    /// </summary>
    public Guid Id { get; set; }
}
using N90.Domain.Common.Entities;
using N90.Domain.Enums;

namespace N90.Domain.Entities;

public abstract class NotificationTemplate : IEntity
{
    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    public string Content { get; set; } = default!;

    public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
    public Guid Id { get; set; }
}
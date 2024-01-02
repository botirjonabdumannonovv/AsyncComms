using N90.Domain.Common.Query;
using N90.Domain.Enums;

namespace N90.Application.Common.Notifications.Models;

public class NotificationTemplateFilter : FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }
}
using N90.Application.Common.Notifications.Models;

namespace N90.Application.Common.Notifications.Events;

public class SendNotificationEvent : NotificationEvent
{
    public NotificationMessage Message { get; set; } = default!;
}
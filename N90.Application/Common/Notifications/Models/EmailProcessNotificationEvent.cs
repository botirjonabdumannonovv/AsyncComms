using N90.Application.Common.Notifications.Events;
using N90.Domain.Enums;

namespace N90.Application.Common.Notifications.Models;

public class EmailProcessNotificationEvent : ProcessNotificationEvent
{
    public EmailProcessNotificationEvent()
    {
        Type = NotificationType.Email;
    }
}
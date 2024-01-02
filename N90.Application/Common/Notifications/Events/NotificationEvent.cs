using N90.Domain.Common.Events;

namespace N90.Application.Common.Notifications.Events;

public class NotificationEvent : Event
{
    public Guid SenderUserId { get; init; }

    public Guid ReceiverUserId { get; init; }
}
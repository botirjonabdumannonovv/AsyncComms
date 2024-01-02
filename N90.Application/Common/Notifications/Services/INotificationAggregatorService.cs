using N90.Application.Common.Notifications.Events;
using N90.Application.Common.Notifications.Models;
using N90.Domain.Common.Exceptions;
using N90.Domain.Entities;

namespace N90.Application.Common.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(ProcessNotificationEvent processNotificationEvent, CancellationToken cancellationToken = default);

    ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    );
}
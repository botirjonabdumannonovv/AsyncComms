using N90.Application.Common.Notifications.Models;
using N90.Domain.Common.Exceptions;

namespace N90.Application.Common.Notifications.Services;

public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(EmailProcessNotificationEvent @event, CancellationToken cancellationToken = default);
}
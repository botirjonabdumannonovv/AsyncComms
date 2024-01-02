using N90.Application.Common.Notifications.Models;

namespace N90.Application.Common.Notifications.Services;

public interface IEmailRenderingService
{
    ValueTask<string> RenderAsync(
        EmailMessage emailMessage,
        // string template,
        // Dictionary<string, string> variables,
        CancellationToken cancellationToken = default
    );
}
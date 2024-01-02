﻿using N90.Application.Common.Notifications.Models;

namespace N90.Application.Common.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
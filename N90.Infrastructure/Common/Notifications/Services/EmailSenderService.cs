﻿using FluentValidation;
using N90.Application.Common.Notifications.Brokers;
using N90.Application.Common.Notifications.Models;
using N90.Application.Common.Notifications.Services;
using N90.Domain.Enums;
using N90.Domain.Extensions;

namespace N90.Infrastructure.Common.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IValidator<EmailMessage> _emailMessageValidator;
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers, IValidator<EmailMessage> emailMessageValidator)
    {
        _emailSenderBrokers = emailSenderBrokers;
        _emailMessageValidator = emailMessageValidator;
    }

    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailMessageValidator.Validate(
            emailMessage,
            options => options.IncludeRuleSets(NotificationProcessingEvent.OnSending.ToString())
        );
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        foreach (var emailSenderBroker in _emailSenderBrokers)
        {
            var sendNotificationTask = () => emailSenderBroker.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            emailMessage.IsSuccessful = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }

        return false;
    }
}
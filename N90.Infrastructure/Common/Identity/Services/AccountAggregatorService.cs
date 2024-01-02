using N90.Application.Common.EventBus.Brokers;
using N90.Application.Common.Identity.Services;
using N90.Application.Common.Notifications.Events;
using N90.Application.Common.Notifications.Models;
using N90.Application.Common.Verifications.Services;
using N90.Domain.Constants;
using N90.Domain.Entities;
using N90.Domain.Enums;

namespace N90.Infrastructure.Common.Identity.Services;

public class AccountAggregatorService(
    IUserService userService,
    IUserSettingsService userSettingsService,
    IUserInfoVerificationCodeService userInfoVerificationCodeService,
    IEventBusBroker eventBusBroker
) : IAccountAggregatorService
{
    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        // Create user and user settings
        user.Role = RoleType.User;
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);
        await userSettingsService.CreateAsync(
            new UserSettings
            {
                Id = createdUser.Id
            },
            cancellationToken: cancellationToken
        );

        // send welcome email
        // var systemUser = await userService.GetSystemUserAsync(cancellationToken: cancellationToken);

        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.WelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, createdUser.FirstName }
            }
        };

        // send welcome email
        await eventBusBroker.PublishAsync(
            welcomeNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        var verificationCode = await userInfoVerificationCodeService.CreateAsync(
            VerificationCodeType.EmailAddressVerification,
            createdUser.Id,
            cancellationToken
        );

        // send verification email
        var sendVerificationEvent = new EmailProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink }
            }
        };

        await eventBusBroker.PublishAsync(
            sendVerificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        // await emailOrchestrationService.SendAsync(
        //     new EmailProcessNotificationEvent
        //     {
        //         SenderUserId = systemUser.Id,
        //         ReceiverUserId = createdUser.Id,
        //         TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
        //         Variables = new Dictionary<string, string>
        //         {
        //             { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink }
        //         }
        //     },
        //     cancellationToken
        // );

        return true;
    }
}
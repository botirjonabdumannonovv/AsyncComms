using AutoMapper;
using N90.Application.Common.Notifications.Events;
using N90.Application.Common.Notifications.Models;

namespace N90.Infrastructure.Common.Mappers;

public class NotificationRequestMapper : Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<ProcessNotificationEvent, EmailProcessNotificationEvent>();
    }
}
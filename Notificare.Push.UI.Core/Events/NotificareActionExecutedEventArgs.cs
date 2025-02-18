using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareActionExecutedEventArgs(
    NotificareNotification notification,
    NotificareNotificationAction action
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationAction Action { get; } = action;
}

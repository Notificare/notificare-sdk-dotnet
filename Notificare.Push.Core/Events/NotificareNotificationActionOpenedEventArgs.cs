using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificareNotificationActionOpenedEventArgs(
    NotificareNotification notification,
    NotificareNotificationAction action
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationAction Action { get; } = action;
}

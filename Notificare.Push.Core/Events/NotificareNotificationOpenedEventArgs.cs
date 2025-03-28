using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificareNotificationOpenedEventArgs(
    NotificareNotification notification
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
}

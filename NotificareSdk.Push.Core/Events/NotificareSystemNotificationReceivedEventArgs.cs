using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificareSystemNotificationReceivedEventArgs(
    NotificareSystemNotification notification
) : EventArgs
{
    public NotificareSystemNotification Notification { get; } = notification;
}

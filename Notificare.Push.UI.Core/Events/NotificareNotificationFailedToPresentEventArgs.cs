using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareNotificationFailedToPresentEventArgs(NotificareNotification notification) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
}

using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareNotificationFinishedPresentingEventArgs(NotificareNotification notification) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
}

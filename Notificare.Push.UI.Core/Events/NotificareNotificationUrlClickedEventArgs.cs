using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareNotificationUrlClickedEventArgs(NotificareNotification notification, string url) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public string Url { get; } = url;
}

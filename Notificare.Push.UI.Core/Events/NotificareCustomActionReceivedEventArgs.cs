using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareCustomActionReceivedEventArgs(
    NotificareNotification notification,
    NotificareNotificationAction action,
    string url
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationAction Action { get; } = action;
    public string Url { get; } = url;
}

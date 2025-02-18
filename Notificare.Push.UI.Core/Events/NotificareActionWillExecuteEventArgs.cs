using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareActionWillExecuteEventArgs(
    NotificareNotification notification,
    NotificareNotificationAction action
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationAction Action { get; } = action;
}

using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Events;

public class NotificareActionFailedToExecuteEventArgs(
    NotificareNotification notification,
    NotificareNotificationAction action,
    string? error
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationAction Action { get; } = action;
    public string? Error { get; } = error;
}

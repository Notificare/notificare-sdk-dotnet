using NotificareSdk.InAppMessaging.Core.Models;

namespace NotificareSdk.InAppMessaging.Core.Events;

public class NotificareActionFailedToExecuteEventArgs(
    NotificareInAppMessage message,
    NotificareInAppMessageAction action,
    string? error
) : EventArgs
{
    public NotificareInAppMessage Message { get; } = message;
    public NotificareInAppMessageAction Action { get; } = action;
    public string? Error { get; } = error;
}

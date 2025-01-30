using NotificareSdk.InAppMessaging.Core.Models;

namespace NotificareSdk.InAppMessaging.Core.Events;

public class NotificareActionExecutedEventArgs(
    NotificareInAppMessage message,
    NotificareInAppMessageAction action
) : EventArgs
{
    public NotificareInAppMessage Message { get; } = message;
    public NotificareInAppMessageAction Action { get; } = action;
}

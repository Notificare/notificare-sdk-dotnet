using NotificareSdk.InAppMessaging.Core.Models;

namespace NotificareSdk.InAppMessaging.Core.Events;

public class NotificareMessageFailedToPresentEventArgs(NotificareInAppMessage message) : EventArgs
{
    public NotificareInAppMessage Message { get; } = message;
}

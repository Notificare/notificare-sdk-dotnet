using NotificareSdk.Core.Models;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificareNotificationSettingsChangedEventArgs(
    bool allowedUI
) : EventArgs
{
    public bool AllowedUI { get; } = allowedUI;
}

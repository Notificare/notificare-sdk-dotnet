using NotificareSdk.Core.Models;

namespace NotificareSdk.Core.Events;

public class NotificareDeviceRegisteredEventArgs(NotificareDevice device) : EventArgs
{
    public NotificareDevice Device { get; } = device;
}

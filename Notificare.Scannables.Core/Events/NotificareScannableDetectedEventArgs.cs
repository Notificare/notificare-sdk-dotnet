using NotificareSdk.Scannables.Core.Models;

namespace NotificareSdk.Scannables.Core.Events;

public class NotificareScannableDetectedEventArgs(NotificareScannable scannable) : EventArgs
{
    public NotificareScannable Scannable { get; } = scannable;
}

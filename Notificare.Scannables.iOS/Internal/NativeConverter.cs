using NotificareSdk.iOS.Internal;
using NotificareSdk.Scannables.Core.Models;

namespace NotificareSdk.Scannables.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareScannable FromNativeScannable(Binding.NotificareScannable scannable)
    {
        return new NotificareScannable(
            id: scannable.ScannableId,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : NotificareNativeConverter.FromNativeNotification(scannable.Notification)
        );
    }
}

using NotificareSdk.Android.Internal;
using NotificareSdk.Scannables.Core.Models;

namespace NotificareSdk.Scannables.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareScannable FromNativeScannable(Binding.Models.NotificareScannable scannable)
    {
        return new NotificareScannable(
            id: scannable.Id,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : NotificareNativeConverter.FromNativeNotification(scannable.Notification)
        );
    }

    internal static Binding.Models.NotificareScannable ToNativeScannable(NotificareScannable scannable)
    {
        return new Binding.Models.NotificareScannable(
            id: scannable.Id,
            name: scannable.Name,
            tag: scannable.Tag,
            type: scannable.Type,
            notification: scannable.Notification == null
                ? null
                : NotificareNativeConverter.ToNativeNotification(scannable.Notification)
        );
    }
}

using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareTransport? FromNativeTransport(
        NotificareSdk.Push.Android.Binding.Models.NotificareTransport transport)
    {
        return Enum.GetValues<NotificareTransport>()
            .First(t => t.ToRawValue() == transport.RawValue);
    }

    internal static NotificareNotificationDeliveryMechanism FromNativeDeliveryMechanism(
        NotificareSdk.Push.Android.Binding.Models.NotificareNotificationDeliveryMechanism deliveryMechanism)
    {
        return Enum.GetValues<NotificareNotificationDeliveryMechanism>()
            .First(dm => dm.ToRawValue() == deliveryMechanism.RawValue);
    }

    internal static NotificarePushSubscription FromNativeSubscription(
        NotificareSdk.Push.Android.Binding.Models.NotificarePushSubscription subscription)
    {
        return new NotificarePushSubscription(
            token: subscription.Token
        );
    }

    internal static NotificareSystemNotification FromNativeSystemNotification(
        NotificareSdk.Push.Android.Binding.Models.NotificareSystemNotification notification)
    {
        return new NotificareSystemNotification(
            id: notification.Id,
            type: notification.Type,
            extra: notification.Extra.ToDictionary(
                kvp => kvp.Key,
                string? (kvp) => kvp.Value
            )
        );
    }
}

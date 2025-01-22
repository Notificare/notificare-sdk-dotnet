using NotificareSdk.iOS.Internal;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareTransport? FromNativeTransport(
        NotificareSdk.Push.iOS.Binding.NotificareTransport transport)
    {
        switch (transport)
        {
            case iOS.Binding.NotificareTransport.Notificare:
                return NotificareTransport.Notificare;
            case iOS.Binding.NotificareTransport.Apns:
                return NotificareTransport.APNS;
            case iOS.Binding.NotificareTransport.Unknown:
                return null;
            default:
                throw new ArgumentException($"Unknown transport: {transport}");
        }
        
        return null;
    }

    internal static NotificareNotificationDeliveryMechanism FromNativeDeliveryMechanism(
        NotificareSdk.Push.iOS.Binding.NotificareNotificationDeliveryMechanism deliveryMechanism)
    {
        switch (deliveryMechanism)
        {
            case iOS.Binding.NotificareNotificationDeliveryMechanism.Standard:
                return NotificareNotificationDeliveryMechanism.Standard;
            case iOS.Binding.NotificareNotificationDeliveryMechanism.Silent:
                return NotificareNotificationDeliveryMechanism.Silent;
            default:
                throw new ArgumentException($"Unknown notification delivery mechanism: {deliveryMechanism}");
        }
    }

    internal static NotificarePushSubscription FromNativeSubscription(
        NotificareSdk.Push.iOS.Binding.NotificarePushSubscription subscription)
    {
        return new NotificarePushSubscription(
            token: subscription.Token
        );
    }

    internal static NotificareSystemNotification FromNativeSystemNotification(
        NotificareSdk.Push.iOS.Binding.NotificareSystemNotification notification)
    {
        return new NotificareSystemNotification(
            id: notification.Id.ToString(),
            type: notification.Type.ToString(),
            extra: NotificareNativeConverter.FromNativeExtraDictionary(notification.Extra).ToDictionary(
                kvp => kvp.Key,
                object? (kvp) => kvp.Value
            )
        );
    }
}

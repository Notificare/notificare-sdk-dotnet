namespace NotificareSdk.Push.Core.Models;

public enum NotificareNotificationDeliveryMechanism
{
    Standard,
    Silent,
}

public static class NotificareNotificationDeliveryMechanismExtensions
{
    public static string ToRawValue(this NotificareNotificationDeliveryMechanism deliveryMechanism)
    {
        return deliveryMechanism switch
        {
            NotificareNotificationDeliveryMechanism.Standard => "standard",
            NotificareNotificationDeliveryMechanism.Silent => "silent",
            _ => throw new ArgumentException(
                $"Unknown {nameof(NotificareNotificationDeliveryMechanism)}: {deliveryMechanism}"
            )
        };
    }
}

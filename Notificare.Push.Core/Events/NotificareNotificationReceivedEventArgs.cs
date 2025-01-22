using NotificareSdk.Core.Models;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificareNotificationReceivedEventArgs(
    NotificareNotification notification,
    NotificareNotificationDeliveryMechanism deliveryMechanism
) : EventArgs
{
    public NotificareNotification Notification { get; } = notification;
    public NotificareNotificationDeliveryMechanism DeliveryMechanism { get; } = deliveryMechanism;
}

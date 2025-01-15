using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Events;

public class NotificarePushSubscriptionChangedEvent(NotificarePushSubscription? subscription) : EventArgs
{
    public NotificarePushSubscription? Subscription { get; } = subscription;
}

using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Internal;

public interface INotificarePushPlatform
{
    void Initialize();

    event EventHandler<NotificareNotificationReceivedEventArgs> NotificationReceived;

    event EventHandler<NotificareSystemNotificationReceivedEventArgs> SystemNotificationReceived;

    // TODO: onUnknownNotificationReceived

    event EventHandler<NotificareNotificationOpenedEventArgs> NotificationOpened;

    // TODO: onUnknownNotificationOpened

    event EventHandler<NotificareNotificationActionOpenedEventArgs> NotificationActionOpened;

    // TODO: onUnknownNotificationActionOpened

    event EventHandler<NotificareNotificationSettingsChangedEventArgs> NotificationSettingsChanged;

    event EventHandler<NotificarePushSubscriptionChangedEvent> SubscriptionChanged;

    // TODO: onShouldOpenNotificationSettings

    // TODO: onFailedToRegisterForRemoteNotifications

    bool HasRemoteNotificationsEnabled { get; }

    NotificareTransport? Transport { get; }

    NotificarePushSubscription? Subscription { get; }

    bool AllowedUI { get; }

    Task EnableRemoteNotificationsAsync();

    Task DisableRemoteNotificationsAsync();

    // TODO: setAuthorizationOptions
    // TODO: setCategoryOptions
    // TODO: setPresentationOptions
}

using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push.Core.Internal;

public interface INotificarePushPlatform
{
    void Initialize();

    event EventHandler<NotificareNotificationReceivedEventArgs> NotificationReceived;

    event EventHandler<NotificareSystemNotificationReceivedEventArgs> SystemNotificationReceived;

    event EventHandler<NotificareUnknownNotificationReceivedEventArgs> UnknownNotificationReceived;

    event EventHandler<NotificareNotificationOpenedEventArgs> NotificationOpened;

    event EventHandler<NotificareUnknownNotificationOpenedEventArgs> UnknownNotificationOpened;

    event EventHandler<NotificareNotificationActionOpenedEventArgs> NotificationActionOpened;

    event EventHandler<NotificareUnknownNotificationActionOpenedEventArgs> UnknownNotificationActionOpened;

    event EventHandler<NotificareNotificationSettingsChangedEventArgs> NotificationSettingsChanged;

    event EventHandler<NotificarePushSubscriptionChangedEventArgs> SubscriptionChanged;

#if IOS
    event EventHandler<NotificareShouldOpenNotificationSettingsEventArgs> ShouldOpenNotificationSettings;
#endif

    bool HasRemoteNotificationsEnabled { get; }

    NotificareTransport? Transport { get; }

    NotificarePushSubscription? Subscription { get; }

    bool AllowedUI { get; }

#if ANDROID
    bool HandleTrampolineIntent(global::Android.Content.Intent intent);
#endif

    Task EnableRemoteNotificationsAsync();

    Task DisableRemoteNotificationsAsync();

    void SetAuthorizationOptions(IList<string> authorizationOptions);

    void SetCategoryOptions(IList<string> categoryOptions);

    void SetPresentationOptions(IList<string> presentationOptions);

#if IOS
    void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken);

    void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error);

    void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    );

    void WillPresentNotification(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification notification,
        Action<UserNotifications.UNNotificationPresentationOptions> completionHandler
    );

    void DidReceiveNotificationResponse(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotificationResponse response,
        Action completionHandler
    );

    void OpenSettings(
        UserNotifications.UNUserNotificationCenter center,
        UserNotifications.UNNotification? notification
    );
#endif
}

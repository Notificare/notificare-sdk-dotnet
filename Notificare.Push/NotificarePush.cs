using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.Core.Internal;
using NotificareSdk.Push.Core.Models;

namespace NotificareSdk.Push;

public static class NotificarePush
{
    private static readonly Lazy<INotificarePushPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificarePushPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    /// <summary>
    /// Called when a push notification is received.
    /// </summary>
    public static event EventHandler<NotificareNotificationReceivedEventArgs> NotificationReceived
    {
        add => Platform.NotificationReceived += value;
        remove => Platform.NotificationReceived -= value;
    }

    /// <summary>
    /// Called when a custom system notification is received.
    /// </summary>
    public static event EventHandler<NotificareSystemNotificationReceivedEventArgs> SystemNotificationReceived
    {
        add => Platform.SystemNotificationReceived += value;
        remove => Platform.SystemNotificationReceived -= value;
    }

    /// <summary>
    /// Called when an unknown notification is received.
    /// </summary>
    public static event EventHandler<NotificareUnknownNotificationReceivedEventArgs> UnknownNotificationReceived
    {
        add => Platform.UnknownNotificationReceived += value;
        remove => Platform.UnknownNotificationReceived -= value;
    }

    /// <summary>
    /// Called when a push notification is opened by the user.
    /// </summary>
    public static event EventHandler<NotificareNotificationOpenedEventArgs> NotificationOpened
    {
        add => Platform.NotificationOpened += value;
        remove => Platform.NotificationOpened -= value;
    }

    /// <summary>
    /// Called when an unknown push notification is opened by the user.
    /// </summary>
    public static event EventHandler<NotificareUnknownNotificationOpenedEventArgs> UnknownNotificationOpened
    {
        add => Platform.UnknownNotificationOpened += value;
        remove => Platform.UnknownNotificationOpened -= value;
    }

    /// <summary>
    /// Called when a push notification action is opened by the user.
    /// </summary>
    public static event EventHandler<NotificareNotificationActionOpenedEventArgs> NotificationActionOpened
    {
        add => Platform.NotificationActionOpened += value;
        remove => Platform.NotificationActionOpened -= value;
    }

    /// <summary>
    /// Called when an unknown push notification action is opened by the user.
    /// </summary>
    public static event EventHandler<NotificareUnknownNotificationActionOpenedEventArgs> UnknownNotificationActionOpened
    {
        add => Platform.UnknownNotificationActionOpened += value;
        remove => Platform.UnknownNotificationActionOpened -= value;
    }

    /// <summary>
    /// Called when the notification settings are changed.
    /// </summary>
    public static event EventHandler<NotificareNotificationSettingsChangedEventArgs> NotificationSettingsChanged
    {
        add => Platform.NotificationSettingsChanged += value;
        remove => Platform.NotificationSettingsChanged -= value;
    }

    /// <summary>
    /// Called when the device's push subscription changes.
    /// </summary>
    public static event EventHandler<NotificarePushSubscriptionChangedEventArgs> SubscriptionChanged
    {
        add => Platform.SubscriptionChanged += value;
        remove => Platform.SubscriptionChanged -= value;
    }

#if IOS
    /// <summary>
    /// Called when a notification prompts the app to open its settings screen.
    /// </summary>
    public static event EventHandler<NotificareShouldOpenNotificationSettingsEventArgs> ShouldOpenNotificationSettings
    {
        add => Platform.ShouldOpenNotificationSettings += value;
        remove => Platform.ShouldOpenNotificationSettings -= value;
    }
#endif


    /// <summary>
    /// Indicates whether remote notifications are enabled.
    /// </summary>
    public static bool HasRemoteNotificationsEnabled => Platform.HasRemoteNotificationsEnabled;

    /// <summary>
    /// Provides the current push transport information.
    /// </summary>
    public static NotificareTransport? Transport => Platform.Transport;

    /// <summary>
    /// Provides the current push subscription.
    /// </summary>
    public static NotificarePushSubscription? Subscription => Platform.Subscription;

    /// <summary>
    /// Indicates whether the device is capable of receiving remote notifications.
    ///
    /// This function returns `true` if the user has granted permission to receive push notifications and the device
    /// has successfully obtained a push token from the notification service. It reflects whether the app can present
    /// notifications as allowed by the system and user settings.
    /// </summary>
    public static bool AllowedUI => Platform.AllowedUI;


#if ANDROID
    /// <summary>
    /// Handles a trampoline intent.
    ///
    /// This method processes an intent and determines if it is a Notificare push notification intent
    /// that requires handling by a trampoline mechanism.
    /// </summary>
    /// <param name="intent">The <see cref="global::Android.Content.Intent"/> to handle.</param>
    /// <returns>
    /// <c>true</c> if the intent was handled, <c>false</c> otherwise.
    /// </returns>
    public static bool HandleTrampolineIntent(global::Android.Content.Intent intent) =>
        Platform.HandleTrampolineIntent(intent);
#endif

    /// <summary>
    /// Enables remote notifications.
    ///
    /// This function enables remote notifications for the application, allowing push notifications to be received.
    ///
    /// <strong>Note</strong>: Starting with Android 13 (API level 33), this function requires the developer to
    /// explicitly request the <c>POST_NOTIFICATIONS</c> permission from the user.
    /// </summary>
    /// <returns></returns>
    public static Task EnableRemoteNotificationsAsync() => Platform.EnableRemoteNotificationsAsync();

    /// <summary>
    /// Disables remote notifications.
    ///
    /// This function disables remote notifications for the application, preventing push notifications from being received.
    /// </summary>
    /// <returns></returns>
    public static Task DisableRemoteNotificationsAsync() => Platform.DisableRemoteNotificationsAsync();

    /// <summary>
    /// Defines the authorization options used when requesting push notification permissions.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="authorizationOptions">The authorization options to be set.</param>
    public static void SetAuthorizationOptions(IList<string> authorizationOptions) =>
        Platform.SetAuthorizationOptions(authorizationOptions);

    /// <summary>
    /// Defines the notification category options for custom notification actions.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="categoryOptions">The category options to be set.</param>
    public static void SetCategoryOptions(IList<string> categoryOptions) =>
        Platform.SetCategoryOptions(categoryOptions);

    /// <summary>
    /// Defines the presentation options for displaying notifications while the app is in the foreground.
    ///
    /// <strong>Note</strong>: This method is only supported on iOS.
    /// </summary>
    /// <param name="presentationOptions">The presentation options to be set.</param>
    public static void SetPresentationOptions(IList<string> presentationOptions) =>
        Platform.SetPresentationOptions(presentationOptions);


    private static INotificarePushPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificarePushPlatformAndroid();
#elif IOS
        return new iOS.NotificarePushPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}

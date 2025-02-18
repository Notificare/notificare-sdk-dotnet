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


    public static event EventHandler<NotificareNotificationReceivedEventArgs> NotificationReceived
    {
        add => Platform.NotificationReceived += value;
        remove => Platform.NotificationReceived -= value;
    }

    public static event EventHandler<NotificareSystemNotificationReceivedEventArgs> SystemNotificationReceived
    {
        add => Platform.SystemNotificationReceived += value;
        remove => Platform.SystemNotificationReceived -= value;
    }

    // TODO: onUnknownNotificationReceived

    public static event EventHandler<NotificareNotificationOpenedEventArgs> NotificationOpened
    {
        add => Platform.NotificationOpened += value;
        remove => Platform.NotificationOpened -= value;
    }

    // TODO: onUnknownNotificationOpened

    public static event EventHandler<NotificareNotificationActionOpenedEventArgs> NotificationActionOpened
    {
        add => Platform.NotificationActionOpened += value;
        remove => Platform.NotificationActionOpened -= value;
    }

    // TODO: onUnknownNotificationActionOpened

    public static event EventHandler<NotificareNotificationSettingsChangedEventArgs> NotificationSettingsChanged
    {
        add => Platform.NotificationSettingsChanged += value;
        remove => Platform.NotificationSettingsChanged -= value;
    }

    public static event EventHandler<NotificarePushSubscriptionChangedEventArgs> SubscriptionChanged
    {
        add => Platform.SubscriptionChanged += value;
        remove => Platform.SubscriptionChanged -= value;
    }

#if IOS
    public static event EventHandler<NotificareShouldOpenNotificationSettingsEventArgs> ShouldOpenNotificationSettings
    {
        add => Platform.ShouldOpenNotificationSettings += value;
        remove => Platform.ShouldOpenNotificationSettings -= value;
    }
#endif


    public static bool HasRemoteNotificationsEnabled => Platform.HasRemoteNotificationsEnabled;

    public static NotificareTransport? Transport => Platform.Transport;

    public static NotificarePushSubscription? Subscription => Platform.Subscription;

    public static bool AllowedUI => Platform.AllowedUI;


#if ANDROID
    public static bool HandleTrampolineIntent(global::Android.Content.Intent intent) =>
        Platform.HandleTrampolineIntent(intent);
#endif

    public static Task EnableRemoteNotificationsAsync() => Platform.EnableRemoteNotificationsAsync();

    public static Task DisableRemoteNotificationsAsync() => Platform.DisableRemoteNotificationsAsync();

    public static void SetAuthorizationOptions(IList<string> authorizationOptions) =>
        Platform.SetAuthorizationOptions(authorizationOptions);

    public static void SetCategoryOptions(IList<string> categoryOptions) =>
        Platform.SetCategoryOptions(categoryOptions);

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

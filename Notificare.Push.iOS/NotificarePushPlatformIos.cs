using NotificareSdk.iOS.Internal;
using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.Core.Internal;
using NotificareSdk.Push.Core.Models;
using NotificareSdk.Push.iOS.Internal;
using UserNotifications;

namespace NotificareSdk.Push.iOS;

public class NotificarePushPlatformIos : INotificarePushPlatform
{
    private InternalNotificarePushDelegate? _delegate;
    private Binding.NotificarePushNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificarePushDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareNotificationReceivedEventArgs>? NotificationReceived;
    public event EventHandler<NotificareSystemNotificationReceivedEventArgs>? SystemNotificationReceived;
    public event EventHandler<NotificareUnknownNotificationReceivedEventArgs>? UnknownNotificationReceived;
    public event EventHandler<NotificareNotificationOpenedEventArgs>? NotificationOpened;
    public event EventHandler<NotificareUnknownNotificationOpenedEventArgs>? UnknownNotificationOpened;
    public event EventHandler<NotificareNotificationActionOpenedEventArgs>? NotificationActionOpened;
    public event EventHandler<NotificareUnknownNotificationActionOpenedEventArgs>? UnknownNotificationActionOpened;
    public event EventHandler<NotificareNotificationSettingsChangedEventArgs>? NotificationSettingsChanged;
    public event EventHandler<NotificarePushSubscriptionChangedEventArgs>? SubscriptionChanged;
    public event EventHandler<NotificareShouldOpenNotificationSettingsEventArgs>? ShouldOpenNotificationSettings;

    public bool HasRemoteNotificationsEnabled => _native.HasRemoteNotificationsEnabled;

    public NotificareTransport? Transport
    {
        get
        {
            var transport = _native.Transport;
            return NativeConverter.FromNativeTransport(transport);
        }
    }

    public NotificarePushSubscription? Subscription
    {
        get
        {
            var subscription = _native.Subscription;
            return subscription == null ? null : NativeConverter.FromNativeSubscription(subscription);
        }
    }

    public bool AllowedUI => _native.AllowedUI;

    public Task EnableRemoteNotificationsAsync()
    {
        TaskCompletionSource completion = new();

        _native.EnableRemoteNotifications(
            _ => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public Task DisableRemoteNotificationsAsync()
    {
        TaskCompletionSource completion = new();

        _native.DisableRemoteNotifications(
            () => completion.TrySetResult(),
            error => completion.TrySetException(new Exception(error.ToString()))
        );

        return completion.Task;
    }

    public void SetAuthorizationOptions(IList<string> authorizationOptions)
    {
        if (authorizationOptions.Count == 0)
        {
            _native.AuthorizationOptions = UNAuthorizationOptions.None;
            return;
        }

        var options = UNAuthorizationOptions.None;

        foreach (var option in authorizationOptions)
        {
            switch (option)
            {
                case "alert":
                    options |= UNAuthorizationOptions.Alert;
                    break;
                case "badge":
                    options |= UNAuthorizationOptions.Badge;
                    break;
                case "sound":
                    options |= UNAuthorizationOptions.Sound;
                    break;
                case "carPlay":
                    options |= UNAuthorizationOptions.CarPlay;
                    break;
                case "providesAppNotificationSettings":
                    options |= UNAuthorizationOptions.ProvidesAppNotificationSettings;
                    break;
                case "provisional":
                    options |= UNAuthorizationOptions.Provisional;
                    break;
                case "criticalAlert":
                    options |= UNAuthorizationOptions.CriticalAlert;
                    break;
                case "announcement":
                    options |= UNAuthorizationOptions.Announcement;
                    break;
            }
        }

        _native.AuthorizationOptions = options;
    }

    public void SetCategoryOptions(IList<string> categoryOptions)
    {
        if (categoryOptions.Count == 0)
        {
            _native.CategoryOptions = UNNotificationCategoryOptions.None;
            return;
        }

        var options = UNNotificationCategoryOptions.None;

        foreach (var option in categoryOptions)
        {
            switch (option)
            {
                case "customDismissAction":
                    options |= UNNotificationCategoryOptions.CustomDismissAction;
                    break;
                case "allowInCarPlay":
                    options |= UNNotificationCategoryOptions.AllowInCarPlay;
                    break;
                case "hiddenPreviewsShowTitle":
                    options |= UNNotificationCategoryOptions.HiddenPreviewsShowTitle;
                    break;
                case "hiddenPreviewsShowSubtitle":
                    options |= UNNotificationCategoryOptions.HiddenPreviewsShowSubtitle;
                    break;
                case "allowAnnouncement":
                    options |= UNNotificationCategoryOptions.AllowAnnouncement;
                    break;
            }
        }

        _native.CategoryOptions = options;
    }

    public void SetPresentationOptions(IList<string> presentationOptions)
    {
        if (presentationOptions.Count == 0)
        {
            _native.PresentationOptions = UNNotificationPresentationOptions.None;
            return;
        }

        var options = UNNotificationPresentationOptions.None;

        foreach (var option in presentationOptions)
        {
#if IOS14_0_OR_GREATER
            if (option is "banner" or "alert")
                options |= UNNotificationPresentationOptions.Banner;

            if (option is "list")
                options |= UNNotificationPresentationOptions.List;
#else
            if (option is "alert") 
                options |= UNNotificationPresentationOptions.Alert;
#endif

            if (option is "badge")
                options |= UNNotificationPresentationOptions.Badge;

            if (option is "sound")
                options |= UNNotificationPresentationOptions.Sound;
        }

        _native.PresentationOptions = options;
    }

    public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
    {
        _native.RegisteredForRemoteNotifications(application, deviceToken);
    }

    public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
    {
        _native.FailedToRegisterForRemoteNotifications(application, error);
    }

    public void DidReceiveRemoteNotification(
        UIApplication application,
        NSDictionary userInfo,
        Action<UIBackgroundFetchResult> completionHandler
    )
    {
        _native.DidReceiveRemoteNotification(application, userInfo, completionHandler);
    }

    public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
    {
        _native.WillPresentNotification(center, notification, completionHandler);
    }

    public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response,
        Action completionHandler)
    {
        _native.DidReceiveNotificationResponse(center, response, completionHandler);
    }

    public void OpenSettings(UNUserNotificationCenter center, UNNotification? notification)
    {
        _native.OpenSettings(center, notification);
    }


    private sealed class InternalNotificarePushDelegate : Binding.NotificarePushNativeBindingDelegate
    {
        private readonly NotificarePushPlatformIos _platform;

        internal InternalNotificarePushDelegate(NotificarePushPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidChangeSubscription(
            Binding.NotificarePushNativeBinding notificarePush,
            Binding.NotificarePushSubscription? subscription
        )
        {
            _platform.SubscriptionChanged?.Invoke(
                _platform,
                new NotificarePushSubscriptionChangedEventArgs(
                    subscription: subscription == null ? null : NativeConverter.FromNativeSubscription(subscription)
                )
            );
        }

        public override void DidChangeNotificationSettings(
            Binding.NotificarePushNativeBinding notificarePush,
            bool allowedUI
        )
        {
            _platform.NotificationSettingsChanged?.Invoke(
                _platform,
                new NotificareNotificationSettingsChangedEventArgs(
                    allowedUI: allowedUI
                )
            );
        }

        public override void DidReceiveNotification(
            Binding.NotificarePushNativeBinding notificarePush,
            NotificareSdk.iOS.Binding.NotificareNotification notification,
            Binding.NotificareNotificationDeliveryMechanism deliveryMechanism
        )
        {
            _platform.NotificationReceived?.Invoke(
                _platform,
                new NotificareNotificationReceivedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification),
                    deliveryMechanism: NativeConverter.FromNativeDeliveryMechanism(deliveryMechanism)
                )
            );
        }

        public override void DidReceiveSystemNotification(
            Binding.NotificarePushNativeBinding notificarePush,
            Binding.NotificareSystemNotification notification
        )
        {
            _platform.SystemNotificationReceived?.Invoke(
                _platform,
                new NotificareSystemNotificationReceivedEventArgs(
                    notification: NativeConverter.FromNativeSystemNotification(notification)
                )
            );
        }

        public override void DidReceiveUnknownNotification(
            Binding.NotificarePushNativeBinding notificarePush,
            NSDictionary userInfo
        )
        {
            _platform.UnknownNotificationReceived?.Invoke(
                _platform,
                new NotificareUnknownNotificationReceivedEventArgs(
                    notification: NotificareNativeConverter.FromNativeExtraDictionary(userInfo)
                )
            );
        }

        public override void ShouldOpenSettings(
            Binding.NotificarePushNativeBinding notificarePush,
            NotificareSdk.iOS.Binding.NotificareNotification? notification
        )
        {
            _platform.ShouldOpenNotificationSettings?.Invoke(
                _platform,
                new NotificareShouldOpenNotificationSettingsEventArgs(
                    notification: notification == null
                        ? null
                        : NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidOpenNotification(
            Binding.NotificarePushNativeBinding notificarePush,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationOpened?.Invoke(
                _platform,
                new NotificareNotificationOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public override void DidOpenUnknownNotification(
            Binding.NotificarePushNativeBinding notificarePush,
            NSDictionary userInfo
        )
        {
            _platform.UnknownNotificationOpened?.Invoke(
                _platform,
                new NotificareUnknownNotificationOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeExtraDictionary(userInfo)
                )
            );
        }

        public override void DidOpenAction(
            Binding.NotificarePushNativeBinding notificarePush,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationActionOpened?.Invoke(
                _platform,
                new NotificareNotificationActionOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification),
                    action: NotificareNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public override void DidOpenUnknownAction(
            Binding.NotificarePushNativeBinding notificarePush,
            string action,
            NSDictionary notification,
            string? responseText
        )
        {
            _platform.UnknownNotificationActionOpened?.Invoke(
                _platform,
                new NotificareUnknownNotificationActionOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeExtraDictionary(notification),
                    action: action,
                    responseText: responseText
                )
            );
        }
    }
}

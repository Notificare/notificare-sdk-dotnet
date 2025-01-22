using Android.Content;
using AndroidX.Lifecycle;
using NotificareSdk.Android.Internal;
using NotificareSdk.Push.Android.Internal;
using NotificareSdk.Push.Core.Events;
using NotificareSdk.Push.Core.Models;
using NotificareSdk.Push.Core.Internal;
using NativeNotificare = NotificareSdk.Push.Android.Binding.NotificarePushCompat;

namespace NotificareSdk.Push.Android;

public class NotificarePushPlatformAndroid : INotificarePushPlatform
{
    private IObserver? _allowedUIObserver;
    private IObserver? _subscriptionObserver;

    public void Initialize()
    {
        NotificareDotNetPushIntentReceiver.Platform = this;
        NativeNotificare.IntentReceiver = Java.Lang.Class.FromType(typeof(NotificareDotNetPushIntentReceiver));

        ObserveAllowedUI();
        ObserveSubscription();
    }

    public event EventHandler<NotificareNotificationReceivedEventArgs>? NotificationReceived;
    public event EventHandler<NotificareSystemNotificationReceivedEventArgs>? SystemNotificationReceived;
    public event EventHandler<NotificareNotificationOpenedEventArgs>? NotificationOpened;
    public event EventHandler<NotificareNotificationActionOpenedEventArgs>? NotificationActionOpened;
    public event EventHandler<NotificareNotificationSettingsChangedEventArgs>? NotificationSettingsChanged;
    public event EventHandler<NotificarePushSubscriptionChangedEvent>? SubscriptionChanged;

    public bool HasRemoteNotificationsEnabled => NativeNotificare.HasRemoteNotificationsEnabled;

    public NotificareTransport? Transport
    {
        get
        {
            var transport = NativeNotificare.Transport;
            return transport == null ? null : NativeConverter.FromNativeTransport(transport);
        }
    }

    public NotificarePushSubscription? Subscription
    {
        get
        {
            var subscription = NativeNotificare.Subscription;
            return subscription == null ? null : NativeConverter.FromNativeSubscription(subscription);
        }
    }

    public bool AllowedUI => NativeNotificare.AllowedUI;

    public bool HandleTrampolineIntent(Intent intent)
    {
        return NativeNotificare.HandleTrampolineIntent(intent);
    }
    
    public async Task EnableRemoteNotificationsAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.EnableRemoteNotifications(callback);

        await callback.Task;
    }

    public async Task DisableRemoteNotificationsAsync()
    {
        var callback = new NotificareAwaitableCallback();
        NativeNotificare.DisableRemoteNotifications(callback);

        await callback.Task;
    }

    public void SetAuthorizationOptions(IList<string> authorizationOptions)
    {
        // no-op
    }

    public void SetCategoryOptions(IList<string> categoryOptions)
    {
        // no-op
    }

    public void SetPresentationOptions(IList<string> presentationOptions)
    {
        // no-op
    }

    private void ObserveAllowedUI()
    {
        if (_allowedUIObserver != null)
        {
            NativeNotificare.ObservableAllowedUI.RemoveObserver(_allowedUIObserver);
        }

        _allowedUIObserver = new AllowedUIObserver(this);
        NativeNotificare.ObservableAllowedUI.ObserveForever(_allowedUIObserver);
    }

    private void ObserveSubscription()
    {
        if (_subscriptionObserver != null)
        {
            NativeNotificare.ObservableSubscription.RemoveObserver(_subscriptionObserver);
        }

        _subscriptionObserver = new SubscriptionObserver(this);
        NativeNotificare.ObservableSubscription.ObserveForever(_subscriptionObserver);
    }


    private class AllowedUIObserver : Java.Lang.Object, IObserver
    {
        private readonly NotificarePushPlatformAndroid _platform;

        internal AllowedUIObserver(NotificarePushPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value == null) return;

            _platform.NotificationSettingsChanged?.Invoke(
                _platform,
                new NotificareNotificationSettingsChangedEventArgs(
                    allowedUI: value == Java.Lang.Boolean.True
                )
            );
        }
    }

    private class SubscriptionObserver : Java.Lang.Object, IObserver
    {
        private readonly NotificarePushPlatformAndroid _platform;

        internal SubscriptionObserver(NotificarePushPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnChanged(Java.Lang.Object? value)
        {
            if (value == null)
            {
                _platform.SubscriptionChanged?.Invoke(
                    _platform,
                    new NotificarePushSubscriptionChangedEvent(
                        subscription: null
                    )
                );

                return;
            }

            if (value is not Binding.Models.NotificarePushSubscription subscription)
            {
                _platform.SubscriptionChanged?.Invoke(
                    _platform,
                    new NotificarePushSubscriptionChangedEvent(
                        subscription: null
                    )
                );

                return;
            }

            _platform.SubscriptionChanged?.Invoke(
                _platform,
                new NotificarePushSubscriptionChangedEvent(
                    subscription: NativeConverter.FromNativeSubscription(subscription)
                )
            );
        }
    }

    [BroadcastReceiver(Enabled = true, Exported = false)]
    private class NotificareDotNetPushIntentReceiver : Binding.NotificarePushIntentReceiver
    {
        internal static NotificarePushPlatformAndroid? Platform;

        protected override void OnNotificationReceived(
            Context context,
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            Binding.Models.NotificareNotificationDeliveryMechanism deliveryMechanism)
        {
            Platform?.NotificationReceived?.Invoke(
                this,
                new NotificareNotificationReceivedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification),
                    deliveryMechanism: NativeConverter.FromNativeDeliveryMechanism(deliveryMechanism)
                )
            );
        }

        protected override void OnSystemNotificationReceived(
            Context context,
            Binding.Models.NotificareSystemNotification notification)
        {
            Platform?.SystemNotificationReceived?.Invoke(
                this,
                new NotificareSystemNotificationReceivedEventArgs(
                    notification: NativeConverter.FromNativeSystemNotification(notification)
                )
            );
        }

        protected override void OnUnknownNotificationReceived(
            Context context,
            Binding.Models.NotificareUnknownNotification notification)
        {
        }

        protected override void OnNotificationOpened(
            Context context,
            NotificareSdk.Android.Binding.Models.NotificareNotification notification)
        {
            Platform?.NotificationOpened?.Invoke(
                this,
                new NotificareNotificationOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        protected override void OnActionOpened(
            Context context,
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            NotificareSdk.Android.Binding.Models.NotificareNotification.Action action)
        {
            Platform?.NotificationActionOpened?.Invoke(
                this,
                new NotificareNotificationActionOpenedEventArgs(
                    notification: NotificareNativeConverter.FromNativeNotification(notification),
                    action: NotificareNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }
    }
}

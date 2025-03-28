using NotificareSdk.Android.Internal;
using NotificareSdk.Core.Models;
using NotificareSdk.Push.UI.Core.Events;
using NotificareSdk.Push.UI.Core.Internal;
using NativeNotificare = NotificareSdk.Push.UI.Android.Binding.NotificarePushUICompat;

namespace NotificareSdk.Push.UI.Android;

public class NotificarePushUIPlatformAndroid : INotificarePushUIPlatform
{
    private Binding.INotificarePushUI.INotificationLifecycleListener? _notificationLifecycleListener;

    public void Initialize()
    {
        ObserveNotificationLifecycleEvents();
    }

    public event EventHandler<NotificareNotificationWillPresentEventArgs>? NotificationWillPresent;
    public event EventHandler<NotificareNotificationPresentedEventArgs>? NotificationPresented;
    public event EventHandler<NotificareNotificationFinishedPresentingEventArgs>? NotificationFinishedPresenting;
    public event EventHandler<NotificareNotificationFailedToPresentEventArgs>? NotificationFailedToPresent;
    public event EventHandler<NotificareNotificationUrlClickedEventArgs>? NotificationUrlClicked;
    public event EventHandler<NotificareActionWillExecuteEventArgs>? ActionWillExecute;
    public event EventHandler<NotificareActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<NotificareActionNotExecutedEventArgs>? ActionNotExecuted;
    public event EventHandler<NotificareActionFailedToExecuteEventArgs>? ActionFailedToExecute;
    public event EventHandler<NotificareCustomActionReceivedEventArgs>? CustomActionReceived;

    public void PresentNotification(
        NotificareNotification notification,
        Activity activity
    )
    {
        NativeNotificare.PresentNotification(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            activity: activity
        );
    }

    public void PresentAction(
        NotificareNotification notification,
        NotificareNotificationAction action,
        Activity activity
    )
    {
        NativeNotificare.PresentAction(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            action: NotificareNativeConverter.ToNativeNotificationAction(action),
            activity: activity
        );
    }

    private void ObserveNotificationLifecycleEvents()
    {
        if (_notificationLifecycleListener != null)
        {
            NativeNotificare.RemoveLifecycleListener(_notificationLifecycleListener);
        }

        _notificationLifecycleListener = new NotificationLifecycleListener(this);
        NativeNotificare.AddLifecycleListener(_notificationLifecycleListener);
    }

    private class NotificationLifecycleListener : Java.Lang.Object,
        Binding.INotificarePushUI.INotificationLifecycleListener
    {
        private readonly NotificarePushUIPlatformAndroid _platform;

        internal NotificationLifecycleListener(NotificarePushUIPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnNotificationWillPresent(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification
        )
        {
            _platform.NotificationWillPresent?.Invoke(
                _platform,
                new NotificareNotificationWillPresentEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationPresented(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification
        )
        {
            _platform.NotificationPresented?.Invoke(
                _platform,
                new NotificareNotificationPresentedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationFinishedPresenting(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification
        )
        {
            _platform.NotificationFinishedPresenting?.Invoke(
                _platform,
                new NotificareNotificationFinishedPresentingEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationFailedToPresent(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification
        )
        {
            _platform.NotificationFailedToPresent?.Invoke(
                _platform,
                new NotificareNotificationFailedToPresentEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void OnNotificationUrlClicked(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            global::Android.Net.Uri uri
        )
        {
            var url = uri.ToString();
            if (url == null) return;

            _platform.NotificationUrlClicked?.Invoke(
                _platform,
                new NotificareNotificationUrlClickedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    url
                )
            );
        }

        public void OnActionWillExecute(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            NotificareSdk.Android.Binding.Models.NotificareNotification.Action action
        )
        {
            _platform.ActionWillExecute?.Invoke(
                _platform,
                new NotificareActionWillExecuteEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public void OnActionExecuted(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            NotificareSdk.Android.Binding.Models.NotificareNotification.Action action
        )
        {
            _platform.ActionExecuted?.Invoke(
                _platform,
                new NotificareActionExecutedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public void OnActionFailedToExecute(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            NotificareSdk.Android.Binding.Models.NotificareNotification.Action action,
            Java.Lang.Exception? error
        )
        {
            _platform.ActionFailedToExecute?.Invoke(
                _platform,
                new NotificareActionFailedToExecuteEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action),
                    error?.ToString()
                )
            );
        }

        public void OnCustomActionReceived(
            NotificareSdk.Android.Binding.Models.NotificareNotification notification,
            NotificareSdk.Android.Binding.Models.NotificareNotification.Action action,
            global::Android.Net.Uri uri
        )
        {
            var url = uri.ToString();
            if (url == null) return;

            _platform.CustomActionReceived?.Invoke(
                _platform,
                new NotificareCustomActionReceivedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action),
                    url
                )
            );
        }
    }
}

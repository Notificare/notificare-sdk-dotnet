using NotificareSdk.Core.Models;
using NotificareSdk.iOS.Internal;
using NotificareSdk.Push.UI.Core.Events;
using NotificareSdk.Push.UI.Core.Internal;

namespace NotificareSdk.Push.UI.iOS;

public class NotificarePushUIPlatformIos : INotificarePushUIPlatform
{
    private InternalNotificarePushUIDelegate? _delegate;
    private Binding.NotificarePushUINativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificarePushUIDelegate(this);
        _native.Delegate = _delegate;
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
        UIViewController controller
    )
    {
        _native.PresentNotification(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            controller: controller
        );
    }

    public void PresentAction(
        NotificareNotification notification,
        NotificareNotificationAction action,
        UIViewController controller
    )
    {
        _native.PresentAction(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            action: NotificareNativeConverter.ToNativeNotificationAction(action),
            controller: controller
        );
    }


    private sealed class InternalNotificarePushUIDelegate : Binding.NotificarePushUINativeBindingDelegate
    {
        private readonly NotificarePushUIPlatformIos _platform;

        internal InternalNotificarePushUIDelegate(NotificarePushUIPlatformIos platform)
        {
            _platform = platform;
        }

        public void WillPresentNotification(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationWillPresent?.Invoke(
                _platform,
                new NotificareNotificationWillPresentEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void DidPresentNotification(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationPresented?.Invoke(
                _platform,
                new NotificareNotificationPresentedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void DidFinishPresentingNotification(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationFinishedPresenting?.Invoke(
                _platform,
                new NotificareNotificationFinishedPresentingEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void DidFailToPresentNotification(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.NotificationFailedToPresent?.Invoke(
                _platform,
                new NotificareNotificationFailedToPresentEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification)
                )
            );
        }

        public void DidClickURL(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NSUrl url,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            var urlStr = url.AbsoluteString;
            if (urlStr == null) return;

            _platform.NotificationUrlClicked?.Invoke(
                _platform,
                new NotificareNotificationUrlClickedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    urlStr
                )
            );
        }

        public void WillExecuteAction(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification
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

        public void DidExecuteAction(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification
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

        public void DidNotExecuteAction(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            _platform.ActionNotExecuted?.Invoke(
                _platform,
                new NotificareActionNotExecutedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action)
                )
            );
        }

        public void DidFailToExecuteAction(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification,
            NSError? error
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

        public void DidReceiveCustomAction(
            Binding.NotificarePushUINativeBinding notificarePushUI,
            NSUrl url,
            NotificareSdk.iOS.Binding.NotificareNotificationAction action,
            NotificareSdk.iOS.Binding.NotificareNotification notification
        )
        {
            var urlStr = url.AbsoluteString;
            if (urlStr == null) return;

            _platform.CustomActionReceived?.Invoke(
                _platform,
                new NotificareCustomActionReceivedEventArgs(
                    NotificareNativeConverter.FromNativeNotification(notification),
                    NotificareNativeConverter.FromNativeNotificationAction(action),
                    urlStr
                )
            );
        }
    }
}

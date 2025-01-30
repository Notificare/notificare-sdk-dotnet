using NotificareSdk.InAppMessaging.Android.Internal;
using NotificareSdk.InAppMessaging.Core.Events;
using NotificareSdk.InAppMessaging.Core.Internal;
using NativeNotificare = NotificareSdk.InAppMessaging.Android.Binding.NotificareInAppMessagingCompat;

namespace NotificareSdk.InAppMessaging.Android;

public class NotificareInAppMessagingPlatformAndroid : INotificareInAppMessagingPlatform
{
    private Binding.INotificareInAppMessaging.IMessageLifecycleListener? _messageLifecycleListener;

    public void Initialize()
    {
        if (_messageLifecycleListener != null)
        {
            NativeNotificare.RemoveLifecycleListener(_messageLifecycleListener);
        }

        _messageLifecycleListener = new LifecycleListener(this);
        NativeNotificare.AddLifecycleListener(_messageLifecycleListener);
    }

    public event EventHandler<NotificareMessagePresentedEventArgs>? MessagePresented;
    public event EventHandler<NotificareMessageFinishedPresentingEventArgs>? MessageFinishedPresenting;
    public event EventHandler<NotificareMessageFailedToPresentEventArgs>? MessageFailedToPresent;
    public event EventHandler<NotificareActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<NotificareActionFailedToExecuteEventArgs>? ActionFailedToExecute;

    public bool HasMessagesSuppressed
    {
        get => NativeNotificare.HasMessagesSuppressed;
        set => NativeNotificare.HasMessagesSuppressed = value;
    }

    public void SetMessagesSuppressed(bool suppressed, bool evaluateContext)
    {
        NativeNotificare.SetMessagesSuppressed(suppressed, evaluateContext);
    }


    private class LifecycleListener : Java.Lang.Object, Binding.INotificareInAppMessaging.IMessageLifecycleListener
    {
        private readonly NotificareInAppMessagingPlatformAndroid _platform;

        internal LifecycleListener(NotificareInAppMessagingPlatformAndroid platform)
        {
            _platform = platform;
        }

        public void OnMessagePresented(
            Binding.Models.NotificareInAppMessage message
        )
        {
            _platform.MessagePresented?.Invoke(
                _platform,
                new NotificareMessagePresentedEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnMessageFinishedPresenting(
            Binding.Models.NotificareInAppMessage message
        )
        {
            _platform.MessageFinishedPresenting?.Invoke(
                _platform,
                new NotificareMessageFinishedPresentingEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnMessageFailedToPresent(
            Binding.Models.NotificareInAppMessage message
        )
        {
            _platform.MessageFailedToPresent?.Invoke(
                _platform,
                new NotificareMessageFailedToPresentEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public void OnActionExecuted(
            Binding.Models.NotificareInAppMessage message,
            Binding.Models.NotificareInAppMessage.Action action
        )
        {
            _platform.ActionExecuted?.Invoke(
                _platform,
                new NotificareActionExecutedEventArgs(
                    NativeConverter.FromNativeMessage(message),
                    NativeConverter.FromNativeMessageAction(action)
                )
            );
        }

        public void OnActionFailedToExecute(
            Binding.Models.NotificareInAppMessage message,
            Binding.Models.NotificareInAppMessage.Action action,
            Java.Lang.Exception? error
        )
        {
            _platform.ActionFailedToExecute?.Invoke(
                _platform,
                new NotificareActionFailedToExecuteEventArgs(
                    NativeConverter.FromNativeMessage(message),
                    NativeConverter.FromNativeMessageAction(action),
                    error?.ToString()
                )
            );
        }
    }
}

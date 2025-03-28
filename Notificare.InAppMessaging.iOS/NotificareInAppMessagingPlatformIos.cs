using NotificareSdk.InAppMessaging.Core.Events;
using NotificareSdk.InAppMessaging.Core.Internal;
using NotificareSdk.InAppMessaging.iOS.Internal;

namespace NotificareSdk.InAppMessaging.iOS;

public class NotificareInAppMessagingPlatformIos : INotificareInAppMessagingPlatform
{
    private InternalNotificareInAppMessagingDelegate? _delegate;
    private Binding.NotificareInAppMessagingNativeBinding _native = new();

    public void Initialize()
    {
        _delegate = new InternalNotificareInAppMessagingDelegate(this);

        _native.Delegate = _delegate;
    }

    public event EventHandler<NotificareMessagePresentedEventArgs>? MessagePresented;
    public event EventHandler<NotificareMessageFinishedPresentingEventArgs>? MessageFinishedPresenting;
    public event EventHandler<NotificareMessageFailedToPresentEventArgs>? MessageFailedToPresent;
    public event EventHandler<NotificareActionExecutedEventArgs>? ActionExecuted;
    public event EventHandler<NotificareActionFailedToExecuteEventArgs>? ActionFailedToExecute;

    public bool HasMessagesSuppressed
    {
        get => _native.HasMessagesSuppressed;
        set => _native.HasMessagesSuppressed = value;
    }

    public void SetMessagesSuppressed(bool suppressed, bool evaluateContext)
    {
        _native.SetMessagesSuppressed(suppressed, evaluateContext);
    }


    private sealed class InternalNotificareInAppMessagingDelegate : Binding.NotificareInAppMessagingNativeBindingDelegate
    {
        private readonly NotificareInAppMessagingPlatformIos _platform;

        internal InternalNotificareInAppMessagingDelegate(NotificareInAppMessagingPlatformIos platform)
        {
            _platform = platform;
        }


        public override void DidPresentMessage(
            Binding.NotificareInAppMessagingNativeBinding notificare,
            Binding.NotificareInAppMessage message
        )
        {
            _platform.MessagePresented?.Invoke(
                _platform,
                new NotificareMessagePresentedEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidFinishPresentingMessage(
            Binding.NotificareInAppMessagingNativeBinding notificare,
            Binding.NotificareInAppMessage message
        )
        {
            _platform.MessageFinishedPresenting?.Invoke(
                _platform,
                new NotificareMessageFinishedPresentingEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidFailToPresentMessage(
            Binding.NotificareInAppMessagingNativeBinding notificare,
            Binding.NotificareInAppMessage message
        )
        {
            _platform.MessageFailedToPresent?.Invoke(
                _platform,
                new NotificareMessageFailedToPresentEventArgs(
                    NativeConverter.FromNativeMessage(message)
                )
            );
        }

        public override void DidExecuteAction(
            Binding.NotificareInAppMessagingNativeBinding notificare,
            Binding.NotificareInAppMessageAction action,
            Binding.NotificareInAppMessage message
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

        public override void DidFailToExecuteAction(
            Binding.NotificareInAppMessagingNativeBinding notificare,
            Binding.NotificareInAppMessageAction action,
            Binding.NotificareInAppMessage message,
            NSError? error
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

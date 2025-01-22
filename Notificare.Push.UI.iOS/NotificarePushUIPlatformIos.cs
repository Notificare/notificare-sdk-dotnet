using NotificareSdk.Core.Models;
using NotificareSdk.iOS.Internal;
using NotificareSdk.Push.UI.Core.Internal;

namespace NotificareSdk.Push.UI.iOS;

public class NotificarePushUIPlatformIos : INotificarePushUIPlatform
{
    // private InternalNotificarePushUIDelegate? _delegate;
    private Binding.NotificarePushUINativeBinding _native = new();

    public void Initialize()
    {
        // TODO: lifecycle listeners
        // _delegate = new InternalNotificarePushUIDelegate(this);
        //
        // _native.Delegate = _delegate;
    }

    public void PresentNotification(NotificareNotification notification, UIViewController controller)
    {
        _native.PresentNotification(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            controller: controller
        );
    }

    public void PresentAction(NotificareNotification notification, NotificareNotificationAction action, UIViewController controller)
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
    }
}

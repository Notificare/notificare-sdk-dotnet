using NotificareSdk.Android.Internal;
using NotificareSdk.Core.Models;
using NotificareSdk.Push.UI.Core.Internal;
using NativeNotificare = NotificareSdk.Push.UI.Android.Binding.NotificarePushUICompat;

namespace NotificareSdk.Push.UI.Android;

public class NotificarePushUIPlatformAndroid : INotificarePushUIPlatform
{
    public void Initialize()
    {
        
    }

    public void PresentNotification(NotificareNotification notification, Activity activity)
    {
        NativeNotificare.PresentNotification(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            activity: activity
        );
    }

    public void PresentAction(NotificareNotification notification, NotificareNotificationAction action, Activity activity)
    {
        NativeNotificare.PresentAction(
            notification: NotificareNativeConverter.ToNativeNotification(notification),
            action: NotificareNativeConverter.ToNativeNotificationAction(action),
            activity: activity
        );
    }
}

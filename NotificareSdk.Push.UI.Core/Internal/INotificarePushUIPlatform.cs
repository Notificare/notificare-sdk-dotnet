using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Internal;

public interface INotificarePushUIPlatform
{
    void Initialize();

#if ANDROID
    void PresentNotification(NotificareNotification notification, Activity activity);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action, Activity activity);
#elif IOS
    void PresentNotification(NotificareNotification notification, UIViewController controller);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action, UIViewController controller);
#endif
}

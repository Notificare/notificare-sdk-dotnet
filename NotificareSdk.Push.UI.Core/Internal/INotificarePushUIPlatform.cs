using NotificareSdk.Core.Models;

namespace NotificareSdk.Push.UI.Core.Internal;

public interface INotificarePushUIPlatform
{
    void Initialize();

#if __IOS__

    void PresentNotification(NotificareNotification notification, UIViewController controller);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action,
        UIViewController controller);

#elif __ANDROID__

    void PresentNotification(NotificareNotification notification, Activity activity);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action, Activity activity);

#endif
}

using NotificareSdk.Core.Models;
using NotificareSdk.Push.UI.Core.Internal;

namespace NotificareSdk.Push.UI;

public static class NotificarePushUI
{
    private static readonly Lazy<INotificarePushUIPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificarePushUIPlatform Platform
    {
        get
        {
            if (Implementation.Value == null)
            {
                throw MissingPlatformSpecificImplementationException();
            }

            return Implementation.Value;
        }
    }


    // TODO: notification lifecycle events


#if __IOS__

    public static void PresentNotification(NotificareNotification notification, UIViewController controller) =>
        Platform.PresentNotification(notification, controller);

    public static void PresentAction(NotificareNotification notification, NotificareNotificationAction action,
        UIViewController controller) => Platform.PresentAction(notification, action, controller);

#elif __ANDROID__

    public static void PresentNotification(NotificareNotification notification, global::Android.App.Activity activity) =>
        Platform.PresentNotification(notification, activity);

    public static void PresentAction(NotificareNotification notification, NotificareNotificationAction action,
        global::Android.App.Activity activity) => Platform.PresentAction(notification, action, activity);

#endif


    private static INotificarePushUIPlatform CreateNotificare()
    {
#if __IOS__
        return new iOS.NotificarePushUIPlatformIos();
#elif __ANDROID__
        return new Android.NotificarePushUIPlatformAndroid();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}

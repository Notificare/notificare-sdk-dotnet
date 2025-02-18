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


#if ANDROID
    public static void PresentNotification(
        NotificareNotification notification, 
        global::Android.App.Activity activity
    ) => Platform.PresentNotification(notification, activity);

    public static void PresentAction(
        NotificareNotification notification, 
        NotificareNotificationAction action,
        global::Android.App.Activity activity
    ) => Platform.PresentAction(notification, action, activity);

#elif IOS
    public static void PresentNotification(
        NotificareNotification notification,
        UIViewController controller
    ) => Platform.PresentNotification(notification, controller);

    public static void PresentAction(
        NotificareNotification notification,
        NotificareNotificationAction action,
        UIViewController controller
    ) => Platform.PresentAction(notification, action, controller);

    public static bool RequiresViewController(this NotificareNotification notification)
    {
        return Platform.RequiresViewController(notification);
    }

#endif


    private static INotificarePushUIPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificarePushUIPlatformAndroid();
#elif IOS
        return new iOS.NotificarePushUIPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}

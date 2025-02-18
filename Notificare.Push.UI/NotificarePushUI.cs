using NotificareSdk.Core.Models;
using NotificareSdk.Push.UI.Core.Events;
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


    public static event EventHandler<NotificareNotificationWillPresentEventArgs> NotificationWillPresent
    {
        add => Platform.NotificationWillPresent += value;
        remove => Platform.NotificationWillPresent -= value;
    }

    public static event EventHandler<NotificareNotificationPresentedEventArgs> NotificationPresented
    {
        add => Platform.NotificationPresented += value;
        remove => Platform.NotificationPresented -= value;
    }

    public static event EventHandler<NotificareNotificationFinishedPresentingEventArgs> NotificationFinishedPresenting
    {
        add => Platform.NotificationFinishedPresenting += value;
        remove => Platform.NotificationFinishedPresenting -= value;
    }

    public static event EventHandler<NotificareNotificationFailedToPresentEventArgs> NotificationFailedToPresent
    {
        add => Platform.NotificationFailedToPresent += value;
        remove => Platform.NotificationFailedToPresent -= value;
    }

    public static event EventHandler<NotificareNotificationUrlClickedEventArgs> NotificationUrlClicked
    {
        add => Platform.NotificationUrlClicked += value;
        remove => Platform.NotificationUrlClicked -= value;
    }

    public static event EventHandler<NotificareActionWillExecuteEventArgs> ActionWillExecute
    {
        add => Platform.ActionWillExecute += value;
        remove => Platform.ActionWillExecute -= value;
    }

    public static event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted
    {
        add => Platform.ActionExecuted += value;
        remove => Platform.ActionExecuted -= value;
    }

    public static event EventHandler<NotificareActionNotExecutedEventArgs> ActionNotExecuted
    {
        add => Platform.ActionNotExecuted += value;
        remove => Platform.ActionNotExecuted -= value;
    }

    public static event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute
    {
        add => Platform.ActionFailedToExecute += value;
        remove => Platform.ActionFailedToExecute -= value;
    }

    public static event EventHandler<NotificareCustomActionReceivedEventArgs> CustomActionReceived
    {
        add => Platform.CustomActionReceived += value;
        remove => Platform.CustomActionReceived -= value;
    }


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

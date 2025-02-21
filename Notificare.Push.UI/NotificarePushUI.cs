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


    /// <summary>
    /// Called when a notification is about to be presented.
    ///
    /// This method is invoked before the notification is shown to the user.
    /// </summary>
    public static event EventHandler<NotificareNotificationWillPresentEventArgs> NotificationWillPresent
    {
        add => Platform.NotificationWillPresent += value;
        remove => Platform.NotificationWillPresent -= value;
    }

    /// <summary>
    /// Called when a notification has been presented.
    ///
    /// This method is triggered when the notification has been shown to the user.
    /// </summary>
    public static event EventHandler<NotificareNotificationPresentedEventArgs> NotificationPresented
    {
        add => Platform.NotificationPresented += value;
        remove => Platform.NotificationPresented -= value;
    }

    /// <summary>
    /// Called when the presentation of a notification has finished.
    ///
    /// This method is invoked after the notification UI has been dismissed or the notification interaction has completed.
    /// </summary>
    public static event EventHandler<NotificareNotificationFinishedPresentingEventArgs> NotificationFinishedPresenting
    {
        add => Platform.NotificationFinishedPresenting += value;
        remove => Platform.NotificationFinishedPresenting -= value;
    }

    /// <summary>
    /// Called when a notification fails to present.
    ///
    /// This method is invoked if there is an error preventing the notification from being presented.
    /// </summary>
    public static event EventHandler<NotificareNotificationFailedToPresentEventArgs> NotificationFailedToPresent
    {
        add => Platform.NotificationFailedToPresent += value;
        remove => Platform.NotificationFailedToPresent -= value;
    }

    /// <summary>
    /// Called when a URL within a notification is clicked.
    ///
    /// This method is triggered when the user clicks a URL in the notification.
    /// </summary>
    public static event EventHandler<NotificareNotificationUrlClickedEventArgs> NotificationUrlClicked
    {
        add => Platform.NotificationUrlClicked += value;
        remove => Platform.NotificationUrlClicked -= value;
    }

    /// <summary>
    /// Called when an action associated with a notification is about to execute.
    ///
    /// This method is invoked right before the action associated with a notification is executed.
    /// </summary>
    public static event EventHandler<NotificareActionWillExecuteEventArgs> ActionWillExecute
    {
        add => Platform.ActionWillExecute += value;
        remove => Platform.ActionWillExecute -= value;
    }

    /// <summary>
    /// Called when an action associated with a notification has been executed.
    ///
    /// This method is triggered after the action associated with the notification has been successfully executed.
    /// </summary>
    public static event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted
    {
        add => Platform.ActionExecuted += value;
        remove => Platform.ActionExecuted -= value;
    }

    /// <summary>
    /// Called when an action associated with a notification is available but has not been executed by the user.
    ///
    /// This method is triggered after the action associated with the notification has not been executed, caused by user interaction.
    /// </summary>
    public static event EventHandler<NotificareActionNotExecutedEventArgs> ActionNotExecuted
    {
        add => Platform.ActionNotExecuted += value;
        remove => Platform.ActionNotExecuted -= value;
    }

    /// <summary>
    /// Called when an action associated with a notification fails to execute.
    ///
    /// This method is triggered if an error occurs while trying to execute an action associated with the notification.
    /// </summary>
    public static event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute
    {
        add => Platform.ActionFailedToExecute += value;
        remove => Platform.ActionFailedToExecute -= value;
    }

    /// <summary>
    /// Called when a custom action associated with a notification is received.
    ///
    /// This method is triggered when a custom action associated with the notification is received, such as a deep link or custom URL scheme.
    /// </summary>
    public static event EventHandler<NotificareCustomActionReceivedEventArgs> CustomActionReceived
    {
        add => Platform.CustomActionReceived += value;
        remove => Platform.CustomActionReceived -= value;
    }


#if ANDROID
    /// <summary>
    /// Presents a notification to the user.
    ///
    /// This method launches the UI for displaying the provided <see cref="NotificareNotification"/>.
    /// </summary>
    /// <param name="notification">The <see cref="NotificareNotification"/> to present.</param>
    /// <param name="activity">The <see cref="Activity"/> in which to present the notification.</param>
    public static void PresentNotification(
        NotificareNotification notification,
        global::Android.App.Activity activity
    ) => Platform.PresentNotification(notification, activity);

    /// <summary>
    /// Presents an action associated with a notification.
    ///
    /// This method presents the UI for executing a specific <see cref="NotificareNotificationAction"/> associated
    /// with the provided <see cref="NotificareNotification"/>.
    /// </summary>
    /// <param name="notification">The <see cref="NotificareNotification"/> to present.</param>
    /// <param name="action">The <see cref="NotificareNotificationAction"/> to execute.</param>
    /// <param name="activity">The <see cref="Activity"/> in which to present the action.</param>
    public static void PresentAction(
        NotificareNotification notification,
        NotificareNotificationAction action,
        global::Android.App.Activity activity
    ) => Platform.PresentAction(notification, action, activity);

#elif IOS
    /// <summary>
    /// Presents a notification to the user.
    ///
    /// This method launches the UI for displaying the provided <see cref="NotificareNotification"/>.
    /// </summary>
    /// <param name="notification">The <see cref="NotificareNotification"/> to present.</param>
    /// <param name="controller">The <see cref="UIViewController"/> in which to present the notification.</param>
    public static void PresentNotification(
        NotificareNotification notification,
        UIViewController controller
    ) => Platform.PresentNotification(notification, controller);

    /// <summary>
    /// Presents an action associated with a notification.
    ///
    /// This method presents the UI for executing a specific <see cref="NotificareNotificationAction"/> associated
    /// with the provided <see cref="NotificareNotification"/>.
    /// </summary>
    /// <param name="notification">The <see cref="NotificareNotification"/> to present.</param>
    /// <param name="action">The <see cref="NotificareNotificationAction"/> to execute.</param>
    /// <param name="controller">The <see cref="UIViewController"/> in which to present the action.</param>
    public static void PresentAction(
        NotificareNotification notification,
        NotificareNotificationAction action,
        UIViewController controller
    ) => Platform.PresentAction(notification, action, controller);

    /// <summary>
    /// Indicates whether the notification requires a parent view controller. 
    /// </summary>
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

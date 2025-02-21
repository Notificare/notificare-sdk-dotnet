using NotificareSdk.InAppMessaging.Core.Events;
using NotificareSdk.InAppMessaging.Core.Internal;

namespace NotificareSdk.InAppMessaging;

public static class NotificareInAppMessaging
{
    private static readonly Lazy<INotificareInAppMessagingPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareInAppMessagingPlatform Platform
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
    /// Called when an in-app message is successfully presented to the user.
    /// </summary>
    public static event EventHandler<NotificareMessagePresentedEventArgs> MessagePresented
    {
        add => Platform.MessagePresented += value;
        remove => Platform.MessagePresented -= value;
    }

    /// <summary>
    /// Called when the presentation of an in-app message has finished.
    ///
    /// This method is invoked after the message is no longer visible to the user.
    /// </summary>
    public static event EventHandler<NotificareMessageFinishedPresentingEventArgs> MessageFinishedPresenting
    {
        add => Platform.MessageFinishedPresenting += value;
        remove => Platform.MessageFinishedPresenting -= value;
    }

    /// <summary>
    /// Called when an in-app message failed to present.
    /// </summary>
    public static event EventHandler<NotificareMessageFailedToPresentEventArgs> MessageFailedToPresent
    {
        add => Platform.MessageFailedToPresent += value;
        remove => Platform.MessageFailedToPresent -= value;
    }

    /// <summary>
    /// Called when an action is successfully executed for an in-app message.
    /// </summary>
    public static event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted
    {
        add => Platform.ActionExecuted += value;
        remove => Platform.ActionExecuted -= value;
    }

    /// <summary>
    /// Called when an action execution failed for an in-app message.
    ///
    /// This method is triggered when an error occurs while attempting to execute an action.
    /// </summary>
    public static event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute
    {
        add => Platform.ActionFailedToExecute += value;
        remove => Platform.ActionFailedToExecute -= value;
    }


    /// <summary>
    /// Indicates whether in-app messages are currently suppressed.
    ///
    /// If <c>true</c>, message dispatching and the presentation of in-app messages are temporarily suppressed.
    /// When <c>false</c>, in-app messages are allowed to be presented.
    /// </summary>
    public static bool HasMessagesSuppressed
    {
        get => Platform.HasMessagesSuppressed;
        set => Platform.HasMessagesSuppressed = value;
    }

    /// <summary>
    /// Sets the message suppression state.
    ///
    /// When messages are suppressed, in-app messages will not be presented to the user.
    /// By default, stopping the in-app message suppression does not re-evaluate the foreground context.
    ///
    /// To trigger a new context evaluation after stopping in-app message suppression,
    /// set the <c>evaluateContext</c> parameter to <c>true</c>.
    /// </summary>
    /// <param name="suppressed">Set to <c>true</c> to suppress in-app messages, or <c>false</c> to stop suppressing them.</param>
    /// <param name="evaluateContext">Set to <c>true</c> to re-evaluate the foreground context when stopping in-app message</param>
    public static void SetMessagesSuppressed(bool suppressed, bool evaluateContext) =>
        Platform.SetMessagesSuppressed(suppressed, evaluateContext);


    private static INotificareInAppMessagingPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareInAppMessagingPlatformAndroid();
#elif IOS
        return new iOS.NotificareInAppMessagingPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}

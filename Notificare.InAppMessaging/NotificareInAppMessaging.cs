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


    public static event EventHandler<NotificareMessagePresentedEventArgs> MessagePresented
    {
        add => Platform.MessagePresented += value;
        remove => Platform.MessagePresented -= value;
    }

    public static event EventHandler<NotificareMessageFinishedPresentingEventArgs> MessageFinishedPresenting
    {
        add => Platform.MessageFinishedPresenting += value;
        remove => Platform.MessageFinishedPresenting -= value;
    }

    public static event EventHandler<NotificareMessageFailedToPresentEventArgs> MessageFailedToPresent
    {
        add => Platform.MessageFailedToPresent += value;
        remove => Platform.MessageFailedToPresent -= value;
    }

    public static event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted
    {
        add => Platform.ActionExecuted += value;
        remove => Platform.ActionExecuted -= value;
    }

    public static event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute
    {
        add => Platform.ActionFailedToExecute += value;
        remove => Platform.ActionFailedToExecute -= value;
    }


    public static bool HasMessagesSuppressed
    {
        get => Platform.HasMessagesSuppressed;
        set => Platform.HasMessagesSuppressed = value;
    }

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

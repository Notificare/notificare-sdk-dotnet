using NotificareSdk.InAppMessaging.Core.Events;

namespace NotificareSdk.InAppMessaging.Core.Internal;

public interface INotificareInAppMessagingPlatform
{
    void Initialize();

    event EventHandler<NotificareMessagePresentedEventArgs> MessagePresented;

    event EventHandler<NotificareMessageFinishedPresentingEventArgs> MessageFinishedPresenting;

    event EventHandler<NotificareMessageFailedToPresentEventArgs> MessageFailedToPresent;

    event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted;

    event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute;

    bool HasMessagesSuppressed { get; set; }

    void SetMessagesSuppressed(bool suppressed, bool evaluateContext);
}

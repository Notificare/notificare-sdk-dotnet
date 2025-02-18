using NotificareSdk.Core.Models;
using NotificareSdk.Push.UI.Core.Events;

namespace NotificareSdk.Push.UI.Core.Internal;

public interface INotificarePushUIPlatform
{
    void Initialize();

    event EventHandler<NotificareNotificationWillPresentEventArgs> NotificationWillPresent;
    event EventHandler<NotificareNotificationPresentedEventArgs> NotificationPresented;
    event EventHandler<NotificareNotificationFinishedPresentingEventArgs> NotificationFinishedPresenting;
    event EventHandler<NotificareNotificationFailedToPresentEventArgs> NotificationFailedToPresent;
    event EventHandler<NotificareNotificationUrlClickedEventArgs> NotificationUrlClicked;
    event EventHandler<NotificareActionWillExecuteEventArgs> ActionWillExecute;
    event EventHandler<NotificareActionExecutedEventArgs> ActionExecuted;
    event EventHandler<NotificareActionNotExecutedEventArgs> ActionNotExecuted;
    event EventHandler<NotificareActionFailedToExecuteEventArgs> ActionFailedToExecute;
    event EventHandler<NotificareCustomActionReceivedEventArgs> CustomActionReceived;
    
#if ANDROID
    void PresentNotification(NotificareNotification notification, Activity activity);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action, Activity activity);
#elif IOS
    void PresentNotification(NotificareNotification notification, UIViewController controller);

    void PresentAction(NotificareNotification notification, NotificareNotificationAction action, UIViewController controller);
#endif
}

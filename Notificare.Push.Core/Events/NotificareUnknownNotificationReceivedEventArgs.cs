namespace NotificareSdk.Push.Core.Events;

public class NotificareUnknownNotificationReceivedEventArgs(
    IDictionary<string, object> notification
) : EventArgs
{
    public IDictionary<string, object> Notification { get; } = notification;
}

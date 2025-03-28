using NotificareSdk.Core.Models;

namespace NotificareSdk.Inbox.Core.Models;

public class NotificareInboxItem
{
    public string Id { get; }
    public NotificareNotification Notification { get; }
    public DateTime Time { get; }
    public bool Opened { get; }
    public DateTime? Expires { get; }

    public NotificareInboxItem(string id, NotificareNotification notification, DateTime time, bool opened,
        DateTime? expires)
    {
        Id = id;
        Notification = notification;
        Time = time;
        Opened = opened;
        Expires = expires;
    }
}

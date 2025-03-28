using NotificareSdk.Core.Models;

namespace NotificareSdk.UserInbox.Core.Models;

public class NotificareUserInboxItem
{
    public string Id { get; }
    public NotificareNotification Notification { get; }
    public DateTime Time { get; }
    public bool Opened { get; }
    public DateTime? Expires { get; }

    public NotificareUserInboxItem(string id, NotificareNotification notification, DateTime time, bool opened,
        DateTime? expires)
    {
        Id = id;
        Notification = notification;
        Time = time;
        Opened = opened;
        Expires = expires;
    }
}

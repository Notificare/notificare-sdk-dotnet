using NotificareSdk.Inbox.Core.Models;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.Inbox.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareInboxItem FromNativeInboxItem(NotificareSdk.Inbox.iOS.Binding.NotificareInboxItem item)
    {
        return new NotificareInboxItem(
            id: item.InboxItemId,
            notification: NotificareNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeSeconds((long)item.Time.SecondsSince1970).DateTime,
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : DateTimeOffset.FromUnixTimeSeconds((long)item.Expires.SecondsSince1970).DateTime
        );
    }

    internal static NotificareSdk.Inbox.iOS.Binding.NotificareInboxItem ToNativeInboxItem(NotificareInboxItem item)
    {
        return new NotificareSdk.Inbox.iOS.Binding.NotificareInboxItem(
            inboxItemId: item.Id,
            notification: NotificareNativeConverter.ToNativeNotification(item.Notification),
            time: NSDate.FromTimeIntervalSince1970(new DateTimeOffset(item.Time).ToUnixTimeSeconds()),
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : NSDate.FromTimeIntervalSince1970(new DateTimeOffset((DateTime)item.Expires).ToUnixTimeSeconds())
        );
    }
}

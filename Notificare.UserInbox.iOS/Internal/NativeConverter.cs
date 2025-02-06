using NotificareSdk.UserInbox.Core.Models;
using NotificareSdk.iOS.Internal;

namespace NotificareSdk.UserInbox.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareUserInboxResponse FromNativeResponse(Binding.NotificareUserInboxResponse response)
    {
        return new NotificareUserInboxResponse(
            count: response.Count.ToInt32(),
            unread: response.Unread.ToInt32(),
            items: response.Items.Select(FromNativeInboxItem).ToList()
        );
    }
    
    private static NotificareUserInboxItem FromNativeInboxItem(Binding.NotificareUserInboxItem item)
    {
        return new NotificareUserInboxItem(
            id: item.InboxItemId,
            notification: NotificareNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeSeconds((long)item.Time.SecondsSince1970).DateTime,
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : DateTimeOffset.FromUnixTimeSeconds((long)item.Expires.SecondsSince1970).DateTime
        );
    }

    internal static Binding.NotificareUserInboxItem ToNativeInboxItem(NotificareUserInboxItem item)
    {
        return new Binding.NotificareUserInboxItem(
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

using NotificareSdk.Android.Internal;
using NotificareSdk.UserInbox.Core.Models;

namespace NotificareSdk.UserInbox.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareUserInboxResponse FromNativeResponse(Binding.Models.NotificareUserInboxResponse response)
    {
        return new NotificareUserInboxResponse(
            count: response.Count,
            unread: response.Unread,
            items: response.Items.Select(FromNativeInboxItem).ToList()
        );
    }

    private static NotificareUserInboxItem FromNativeInboxItem(Binding.Models.NotificareUserInboxItem item)
    {
        return new NotificareUserInboxItem(
            id: item.Id,
            notification: NotificareNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeMilliseconds(item.Time.Time).DateTime,
            opened: item.Opened,
            expires: item.Expires == null ? null : DateTimeOffset.FromUnixTimeMilliseconds(item.Expires.Time).DateTime
        );
    }

    internal static Binding.Models.NotificareUserInboxItem ToNativeInboxItem(NotificareUserInboxItem item)
    {
        return new Binding.Models.NotificareUserInboxItem(
            id: item.Id,
            notification: NotificareNativeConverter.ToNativeNotification(item.Notification),
            time: new Java.Util.Date(new DateTimeOffset(item.Time).ToUnixTimeMilliseconds()),
            opened: item.Opened,
            expires: item.Expires == null
                ? null
                : new Java.Util.Date(new DateTimeOffset((DateTime)item.Expires).ToUnixTimeMilliseconds())
        );
    }
}

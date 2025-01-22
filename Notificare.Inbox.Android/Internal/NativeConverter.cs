using NotificareSdk.Android.Internal;
using NotificareSdk.Inbox.Core.Models;

namespace NotificareSdk.Inbox.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareInboxItem FromNativeInboxItem(
        NotificareSdk.Inbox.Android.Binding.Models.NotificareInboxItem item)
    {
        return new NotificareInboxItem(
            id: item.Id,
            notification: NotificareNativeConverter.FromNativeNotification(item.Notification),
            time: DateTimeOffset.FromUnixTimeMilliseconds(item.Time.Time).DateTime,
            opened: item.Opened,
            expires: item.Expires == null ? null : DateTimeOffset.FromUnixTimeMilliseconds(item.Expires.Time).DateTime
        );
    }

    internal static NotificareSdk.Inbox.Android.Binding.Models.NotificareInboxItem ToNativeInboxItem(
        NotificareInboxItem item)
    {
        return new NotificareSdk.Inbox.Android.Binding.Models.NotificareInboxItem(
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

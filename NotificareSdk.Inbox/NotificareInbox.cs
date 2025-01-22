using NotificareSdk.Core.Models;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Inbox.Core.Internal;
using NotificareSdk.Inbox.Core.Models;

namespace NotificareSdk.Inbox;

public static class NotificareInbox
{
    private static readonly Lazy<INotificareInboxPlatform> Implementation = new(() =>
    {
        var instance = CreateNotificare();
        instance.Initialize();

        return instance;
    });

    private static INotificareInboxPlatform Platform
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


    public static event EventHandler<NotificareInboxUpdatedEventArgs> InboxUpdated
    {
        add => Platform.InboxUpdated += value;
        remove => Platform.InboxUpdated -= value;
    }

    public static event EventHandler<NotificareBadgeUpdatedEventArgs> BadgeUpdated
    {
        add => Platform.BadgeUpdated += value;
        remove => Platform.BadgeUpdated -= value;
    }


    public static IList<NotificareInboxItem> Items => Platform.Items;

    public static int Badge => Platform.Badge;

    public static void Refresh() => Platform.Refresh();

    public static Task<NotificareNotification> OpenAsync(NotificareInboxItem item) => Platform.OpenAsync(item);

    public static Task MarkAsReadAsync(NotificareInboxItem item) => Platform.MarkAsReadAsync(item);

    public static Task MarkAllAsReadAsync() => Platform.MarkAllAsReadAsync();

    public static Task RemoveAsync(NotificareInboxItem item) => Platform.RemoveAsync(item);

    public static Task ClearAsync() => Platform.ClearAsync();


    private static INotificareInboxPlatform CreateNotificare()
    {
#if ANDROID
        return new Android.NotificareInboxPlatformAndroid();
#elif IOS
        return new iOS.NotificareInboxPlatformIos();
#endif
    }

    private static NotImplementedException MissingPlatformSpecificImplementationException()
    {
        return new NotImplementedException("Unable to load the platform-specific implementation of Notificare.");
    }
}

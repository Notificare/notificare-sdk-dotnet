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


    /// <summary>
    /// Called when the inbox is successfully updated.
    /// </summary>
    public static event EventHandler<NotificareInboxUpdatedEventArgs> InboxUpdated
    {
        add => Platform.InboxUpdated += value;
        remove => Platform.InboxUpdated -= value;
    }

    /// <summary>
    /// Called when the unread message count badge is updated.
    /// </summary>
    public static event EventHandler<NotificareBadgeUpdatedEventArgs> BadgeUpdated
    {
        add => Platform.BadgeUpdated += value;
        remove => Platform.BadgeUpdated -= value;
    }


    /// <summary>
    /// A list of all <see cref="NotificareInboxItem"/>, sorted by the timestamp.
    /// </summary>
    public static IList<NotificareInboxItem> Items => Platform.Items;

    /// <summary>
    /// The current badge count, representing the number of unread inbox items.
    /// </summary>
    public static int Badge => Platform.Badge;

    /// <summary>
    /// Refreshes the inbox data, ensuring the items and badge count reflect the latest server state.
    /// </summary>
    public static void Refresh() => Platform.Refresh();

    /// <summary>
    /// Opens a specified inbox item, marking it as read and returning the associated notification.
    /// </summary>
    /// <param name="item">The <see cref="NotificareInboxItem"/> to open.</param>
    /// <returns>
    /// A task that resolves to the <see cref="NotificareNotification"/> associated with the inbox item.
    /// </returns>
    public static Task<NotificareNotification> OpenAsync(NotificareInboxItem item) => Platform.OpenAsync(item);

    /// <summary>
    /// Marks the specified inbox item as read.
    /// </summary>
    /// <param name="item">The <see cref="NotificareInboxItem"/> to mark as read.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully marked as read.
    /// </returns>
    public static Task MarkAsReadAsync(NotificareInboxItem item) => Platform.MarkAsReadAsync(item);

    /// <summary>
    /// Marks all inbox items as read.
    /// </summary>
    /// <returns>
    /// A task that resolves when all inbox items have been successfully marked as read.
    /// </returns>
    public static Task MarkAllAsReadAsync() => Platform.MarkAllAsReadAsync();

    /// <summary>
    /// Permanently removes the specified inbox item from the inbox.
    /// </summary>
    /// <param name="item">The <see cref="NotificareInboxItem"/> to remove.</param>
    /// <returns>
    /// A task that resolves when the inbox item has been successfully removed.
    /// </returns>
    public static Task RemoveAsync(NotificareInboxItem item) => Platform.RemoveAsync(item);

    /// <summary>
    /// Clears all inbox items, permanently deleting them from the inbox.
    /// </summary>
    /// <returns>
    /// A task that resolves when all inbox items have been successfully cleared.
    /// </returns>
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

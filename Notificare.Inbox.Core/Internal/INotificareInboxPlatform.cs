using NotificareSdk.Core.Models;
using NotificareSdk.Inbox.Core.Events;
using NotificareSdk.Inbox.Core.Models;

namespace NotificareSdk.Inbox.Core.Internal;

public interface INotificareInboxPlatform
{
    void Initialize();

    event EventHandler<NotificareInboxUpdatedEventArgs> InboxUpdated;

    event EventHandler<NotificareBadgeUpdatedEventArgs> BadgeUpdated;

    IList<NotificareInboxItem> Items { get; }

    int Badge { get; }

    void Refresh();

    Task<NotificareNotification> OpenAsync(NotificareInboxItem item);

    Task MarkAsReadAsync(NotificareInboxItem item);

    Task MarkAllAsReadAsync();

    Task RemoveAsync(NotificareInboxItem item);

    Task ClearAsync();
}

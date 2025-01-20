using NotificareSdk.Inbox.Core.Models;

namespace NotificareSdk.Inbox.Core.Events;

public class NotificareInboxUpdatedEventArgs(IList<NotificareInboxItem> items) : EventArgs
{
    public IList<NotificareInboxItem> Items { get; } = items;
}

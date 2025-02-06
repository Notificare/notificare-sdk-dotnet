namespace NotificareSdk.UserInbox.Core.Models;

public class NotificareUserInboxResponse
{
    public int Count { get; }
    public int Unread { get; }
    public IList<NotificareUserInboxItem> Items { get; }

    public NotificareUserInboxResponse(int count, int unread, IList<NotificareUserInboxItem> items)
    {
        Count = count;
        Unread = unread;
        Items = items;
    }
}

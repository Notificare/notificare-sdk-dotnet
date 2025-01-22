namespace NotificareSdk.Inbox.Core.Events;

public class NotificareBadgeUpdatedEventArgs(int badge) : EventArgs
{
    public int Badge { get; } = badge;
}

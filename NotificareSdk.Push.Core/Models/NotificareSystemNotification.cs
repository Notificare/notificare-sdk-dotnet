namespace NotificareSdk.Push.Core.Models;

public class NotificareSystemNotification
{
    public string Id { get; }

    public string Type { get; }

    public IDictionary<string, string?> Extra { get; }

    public NotificareSystemNotification(string id, string type, IDictionary<string, string?> extra)
    {
        Id = id;
        Type = type;
        Extra = extra;
    }
}

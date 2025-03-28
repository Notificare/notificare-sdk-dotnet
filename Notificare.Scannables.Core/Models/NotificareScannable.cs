using NotificareSdk.Core.Models;

namespace NotificareSdk.Scannables.Core.Models;

public class NotificareScannable
{
    public string Id { get; }
    public string Name { get; }
    public string Tag { get; }
    public string Type { get; }
    public NotificareNotification? Notification { get; }

    public NotificareScannable(string id, string name, string tag, string type, NotificareNotification? notification)
    {
        Id = id;
        Name = name;
        Tag = tag;
        Type = type;
        Notification = notification;
    }
}

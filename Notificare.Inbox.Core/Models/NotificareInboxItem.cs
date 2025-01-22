using Newtonsoft.Json;
using NotificareSdk.Core.Models;

namespace NotificareSdk.Inbox.Core.Models;

public class NotificareInboxItem
{
    [JsonProperty(propertyName: "id")] public string Id { get; }

    [JsonProperty(propertyName: "notification")]
    public NotificareNotification Notification { get; }

    [JsonProperty(propertyName: "time")] public DateTime Time { get; }

    [JsonProperty(propertyName: "opened")] public bool Opened { get; }

    [JsonProperty(propertyName: "expires")]
    public DateTime? Expires { get; }

    public NotificareInboxItem(string id, NotificareNotification notification, DateTime time, bool opened,
        DateTime? expires)
    {
        Id = id;
        Notification = notification;
        Time = time;
        Opened = opened;
        Expires = expires;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareInboxItem FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareInboxItem>(json)
               ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

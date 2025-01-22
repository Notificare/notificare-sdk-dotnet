using Newtonsoft.Json;

namespace NotificareSdk.Core.Models;

public class NotificareDevice
{
    [JsonProperty(propertyName: "id")]
    public string Id { get; }

    [JsonProperty(propertyName: "userId")]
    public string? UserId { get; }

    [JsonProperty(propertyName: "userName")]
    public string? UserName { get; }

    [JsonProperty(propertyName: "timeZoneOffset")]
    public double TimeZoneOffset { get; }

    [JsonProperty(propertyName: "dnd")]
    public NotificareDoNotDisturb? Dnd { get; }

    [JsonProperty(propertyName: "userData")]
    public IDictionary<string, string> UserData { get; }

    public NotificareDevice(string id, string? userId, string? userName, double timeZoneOffset, NotificareDoNotDisturb? dnd, IDictionary<string, string> userData)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        TimeZoneOffset = timeZoneOffset;
        Dnd = dnd;
        UserData = userData;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareDevice FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareDevice>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

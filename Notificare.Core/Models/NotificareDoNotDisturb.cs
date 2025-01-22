using Newtonsoft.Json;

namespace NotificareSdk.Core.Models;

public class NotificareDoNotDisturb
{
    [JsonProperty(propertyName: "start")]
    public NotificareTime Start;

    [JsonProperty(propertyName: "end")]
    public NotificareTime End;

    public NotificareDoNotDisturb(NotificareTime start, NotificareTime end)
    {
        Start = start;
        End = end;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareDoNotDisturb FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareDoNotDisturb>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

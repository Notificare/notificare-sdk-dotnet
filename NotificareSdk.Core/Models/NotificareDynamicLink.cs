using Newtonsoft.Json;

namespace NotificareSdk.Core.Models;

public class NotificareDynamicLink
{
    [JsonProperty(propertyName: "target")]
    public string Target { get; }

    public NotificareDynamicLink(string target)
    {
        Target = target;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificareDynamicLink FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificareDynamicLink>(json)
            ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

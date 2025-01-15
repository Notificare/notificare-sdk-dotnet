using Newtonsoft.Json;

namespace NotificareSdk.Push.Core.Models;

public class NotificarePushSubscription
{
    [JsonProperty(propertyName: "token")] 
    public string? Token { get; }

    public NotificarePushSubscription(string? token)
    {
        Token = token;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public static NotificarePushSubscription FromJson(string json)
    {
        return JsonConvert.DeserializeObject<NotificarePushSubscription>(json)
               ?? throw new ArgumentException("JSON decoding result cannot be null.");
    }
}

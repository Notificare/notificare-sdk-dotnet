using Newtonsoft.Json;
using NotificareSdk.Core.Models;

namespace NotificareSdk.Core.Converters;

public class NotificareTimeJsonConverter : JsonConverter<NotificareTime>
{
    public override NotificareTime? ReadJson(JsonReader reader, Type objectType, NotificareTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
        {
            return null;
        }

        string s = (string)reader.Value;

        return NotificareTime.FromJson(s);
    }

    public override void WriteJson(JsonWriter writer, NotificareTime? value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToJson());
    }
}

using System.Runtime.Serialization.Json;

namespace Notificare.Build.Tasks.ProcessNotificareServices;

internal static class NotificareServicesLoader
{
    public static NotificareServices ProcessJson(Stream json)
    {
        var serializer = new DataContractJsonSerializer(typeof(NotificareServices));

        if (serializer.ReadObject(json) is not NotificareServices notificareServices)
            throw new NullReferenceException();

        return notificareServices;
    }
}

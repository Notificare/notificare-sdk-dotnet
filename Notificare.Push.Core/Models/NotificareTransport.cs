namespace NotificareSdk.Push.Core.Models;

public enum NotificareTransport
{
    Notificare,
    GCM,
    APNS,
}

public static class NotificareTransportExtensions
{
    public static string ToRawValue(this NotificareTransport transport)
    {
        return transport switch
        {
            NotificareTransport.Notificare => "Notificare",
            NotificareTransport.GCM => "GCM",
            NotificareTransport.APNS => "APNS",
            _ => throw new ArgumentException($"Unknown {nameof(NotificareTransport)}: {transport}")
        };
    }
}

using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareBeaconEnteredEventArgs(NotificareBeacon beacon) : EventArgs
{
    public NotificareBeacon Beacon { get; } = beacon;
}

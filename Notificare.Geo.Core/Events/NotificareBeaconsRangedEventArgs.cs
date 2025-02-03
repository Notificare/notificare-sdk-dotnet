using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareBeaconsRangedEventArgs(NotificareRegion region, IList<NotificareBeacon> beacons) : EventArgs
{
    public NotificareRegion Region { get; } = region;
    public IList<NotificareBeacon> Beacons { get; } = beacons;
}

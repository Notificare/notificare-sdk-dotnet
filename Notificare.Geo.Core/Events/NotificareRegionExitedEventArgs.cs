using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareRegionExitedEventArgs(NotificareRegion region) : EventArgs
{
    public NotificareRegion Region { get; } = region;
}

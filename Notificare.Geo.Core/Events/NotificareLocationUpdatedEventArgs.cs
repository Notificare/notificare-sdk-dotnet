using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareLocationUpdatedEventArgs(NotificareLocation location) : EventArgs
{
    public NotificareLocation Location { get; } = location;
}

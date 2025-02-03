using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareHeadingUpdatedEventArgs(NotificareHeading heading) : EventArgs
{
    public NotificareHeading Heading { get; } = heading;
}

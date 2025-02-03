using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Core.Events;

public class NotificareVisitEventArgs(NotificareVisit visit) : EventArgs
{
    public NotificareVisit Visit { get; } = visit;
}

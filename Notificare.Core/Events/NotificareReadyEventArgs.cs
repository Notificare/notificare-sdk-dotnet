using NotificareSdk.Core.Models;

namespace NotificareSdk.Core.Events;

public class NotificareReadyEventArgs(NotificareApplication application) : EventArgs
{
    public NotificareApplication Application { get; } = application;
}

namespace NotificareSdk.Core.Models;

public class NotificareDoNotDisturb
{
    public NotificareTime Start;
    public NotificareTime End;

    public NotificareDoNotDisturb(NotificareTime start, NotificareTime end)
    {
        Start = start;
        End = end;
    }
}

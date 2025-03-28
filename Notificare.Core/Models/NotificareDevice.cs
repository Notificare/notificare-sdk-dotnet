namespace NotificareSdk.Core.Models;

public class NotificareDevice
{
    public string Id { get; }
    public string? UserId { get; }
    public string? UserName { get; }
    public double TimeZoneOffset { get; }
    public NotificareDoNotDisturb? Dnd { get; }
    public IDictionary<string, string> UserData { get; }

    public NotificareDevice(string id, string? userId, string? userName, double timeZoneOffset, NotificareDoNotDisturb? dnd, IDictionary<string, string> userData)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        TimeZoneOffset = timeZoneOffset;
        Dnd = dnd;
        UserData = userData;
    }
}

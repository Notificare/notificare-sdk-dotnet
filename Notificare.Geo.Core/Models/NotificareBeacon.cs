namespace NotificareSdk.Geo.Core.Models;

public class NotificareBeacon
{
    public string Id { get; }
    public string Name { get; }
    public int Major { get; }
    public int? Minor { get; }
    public bool Triggers { get; }
    public NotificareBeaconProximity Proximity { get; }

    public NotificareBeacon(string id, string name, int major, int? minor, bool triggers,
        NotificareBeaconProximity proximity)
    {
        Id = id;
        Name = name;
        Major = major;
        Minor = minor;
        Triggers = triggers;
        Proximity = proximity;
    }
}

public enum NotificareBeaconProximity
{
    Unknown,
    Immediate,
    Near,
    Far
}

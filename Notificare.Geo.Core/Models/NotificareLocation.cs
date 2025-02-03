namespace NotificareSdk.Geo.Core.Models;

public class NotificareLocation
{
    public double Latitude { get; }
    public double Longitude { get; }
    public double Altitude { get; }
    public double Course { get; }
    public double Speed { get; }
    public int? Floor { get; }
    public double HorizontalAccuracy { get; }
    public double VerticalAccuracy { get; }
    public DateTime Timestamp { get; }

    public NotificareLocation(double latitude, double longitude, double altitude, double course, double speed,
        int? floor, double horizontalAccuracy, double verticalAccuracy, DateTime timestamp)
    {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
        Course = course;
        Speed = speed;
        Floor = floor;
        HorizontalAccuracy = horizontalAccuracy;
        VerticalAccuracy = verticalAccuracy;
        Timestamp = timestamp;
    }
}

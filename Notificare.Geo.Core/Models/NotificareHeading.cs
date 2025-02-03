namespace NotificareSdk.Geo.Core.Models;

public class NotificareHeading
{
    public double MagneticHeading { get; }
    public double TrueHeading { get; }
    public double HeadingAccuracy { get; }
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public DateTime Timestamp { get; }

    public NotificareHeading(double magneticHeading, double trueHeading, double headingAccuracy, double x, double y,
        double z, DateTime timestamp)
    {
        MagneticHeading = magneticHeading;
        TrueHeading = trueHeading;
        HeadingAccuracy = headingAccuracy;
        X = x;
        Y = y;
        Z = z;
        Timestamp = timestamp;
    }
}

namespace NotificareSdk.Geo.Core.Models;

public class NotificareVisit
{
    public DateTime DepartureDate { get; }
    public DateTime ArrivalDate { get; }
    public double Latitude { get; }
    public double Longitude { get; }

    public NotificareVisit(DateTime departureDate, DateTime arrivalDate, double latitude, double longitude)
    {
        DepartureDate = departureDate;
        ArrivalDate = arrivalDate;
        Latitude = latitude;
        Longitude = longitude;
    }
}

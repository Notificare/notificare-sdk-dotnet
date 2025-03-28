namespace NotificareSdk.Geo.Core.Models;

public class NotificareRegion
{
    public string Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public string? ReferenceKey { get; }
    public NotificareRegionGeometry Geometry { get; }
    public NotificareRegionAdvancedGeometry? AdvancedGeometry { get; }
    public int? Major { get; }
    public double Distance { get; }
    public string TimeZone { get; }
    public double TimeZoneOffset { get; }

    public NotificareRegion(string id, string name, string? description, string? referenceKey,
        NotificareRegionGeometry geometry, NotificareRegionAdvancedGeometry? advancedGeometry, int? major,
        double distance, string timeZone, double timeZoneOffset)
    {
        Id = id;
        Name = name;
        Description = description;
        ReferenceKey = referenceKey;
        Geometry = geometry;
        AdvancedGeometry = advancedGeometry;
        Major = major;
        Distance = distance;
        TimeZone = timeZone;
        TimeZoneOffset = timeZoneOffset;
    }
}

public class NotificareRegionGeometry
{
    public string Type { get; }
    public NotificareRegionCoordinate Coordinate { get; }

    public NotificareRegionGeometry(string type, NotificareRegionCoordinate coordinate)
    {
        Type = type;
        Coordinate = coordinate;
    }
}

public class NotificareRegionAdvancedGeometry
{
    public string Type { get; }
    public IList<NotificareRegionCoordinate> Coordinates { get; }

    public NotificareRegionAdvancedGeometry(string type, IList<NotificareRegionCoordinate> coordinates)
    {
        Type = type;
        Coordinates = coordinates;
    }
}

public class NotificareRegionCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }

    public NotificareRegionCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}

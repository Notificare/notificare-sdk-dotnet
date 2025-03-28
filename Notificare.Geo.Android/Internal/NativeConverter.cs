using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.Android.Internal;

internal static class NativeConverter
{
    internal static NotificareLocation FromNativeLocation(
        Binding.Models.NotificareLocation location
    )
    {
        return new NotificareLocation(
            latitude: location.Latitude,
            longitude: location.Longitude,
            altitude: location.Altitude,
            course: location.Course,
            speed: location.Speed,
            floor: null,
            horizontalAccuracy: location.HorizontalAccuracy,
            verticalAccuracy: location.VerticalAccuracy,
            timestamp: DateTimeOffset.FromUnixTimeMilliseconds(location.Timestamp.Time).DateTime
        );
    }

    internal static NotificareRegion FromNativeRegion(
        Binding.Models.NotificareRegion region
    )
    {
        return new NotificareRegion(
            id: region.Id,
            name: region.Name,
            description: region.Description,
            referenceKey: region.ReferenceKey,
            geometry: FromNativeRegionGeometry(region.GetGeometry()),
            advancedGeometry: region.GetAdvancedGeometry() == null
                ? null
                : FromNativeRegionAdvancedGeometry(region.GetAdvancedGeometry()!),
            major: region.Major?.IntValue(),
            distance: region.Distance,
            timeZone: region.TimeZone,
            timeZoneOffset: region.TimeZoneOffset
        );
    }

    private static NotificareRegionGeometry FromNativeRegionGeometry(
        Binding.Models.NotificareRegion.Geometry geometry
    )
    {
        return new NotificareRegionGeometry(
            type: geometry.Type,
            coordinate: FromNativeRegionCoordinate(geometry.Coordinate)
        );
    }

    private static NotificareRegionAdvancedGeometry FromNativeRegionAdvancedGeometry(
        Binding.Models.NotificareRegion.AdvancedGeometry geometry
    )
    {
        return new NotificareRegionAdvancedGeometry(
            type: geometry.Type,
            coordinates: geometry.Coordinates.Select(FromNativeRegionCoordinate).ToList()
        );
    }

    private static NotificareRegionCoordinate FromNativeRegionCoordinate(
        Binding.Models.NotificareRegion.Coordinate coordinate
    )
    {
        return new NotificareRegionCoordinate(
            latitude: coordinate.Latitude,
            longitude: coordinate.Longitude
        );
    }

    internal static NotificareBeacon FromNativeBeacon(
        Binding.Models.NotificareBeacon beacon
    )
    {
        return new NotificareBeacon(
            id: beacon.Id,
            name: beacon.Name,
            major: beacon.Major,
            minor: beacon.Minor?.IntValue(),
            triggers: beacon.Triggers,
            proximity: FromNativeBeaconProximity(beacon.GetProximity())
        );
    }

    internal static NotificareBeaconProximity FromNativeBeaconProximity(
        Binding.Models.NotificareBeacon.Proximity proximity
    )
    {
        if (proximity == Binding.Models.NotificareBeacon.Proximity.Unknown)
            return NotificareBeaconProximity.Unknown;

        if (proximity == Binding.Models.NotificareBeacon.Proximity.Immediate)
            return NotificareBeaconProximity.Immediate;

        if (proximity == Binding.Models.NotificareBeacon.Proximity.Near)
            return NotificareBeaconProximity.Near;

        if (proximity == Binding.Models.NotificareBeacon.Proximity.Far)
            return NotificareBeaconProximity.Far;

        throw new ArgumentException($"Unknown beacon proximity: {proximity}");
    }
}

using NotificareSdk.Geo.Core.Models;

namespace NotificareSdk.Geo.iOS.Internal;

internal static class NativeConverter
{
    internal static NotificareLocation FromNativeLocation(
        NotificareSdk.Geo.iOS.Binding.NotificareLocation location
    )
    {
        return new NotificareLocation(
            latitude: location.Latitude,
            longitude: location.Longitude,
            altitude: location.Altitude,
            course: location.Course,
            speed: location.Speed,
            floor: location.Floor == null ? null : location.Floor.Int32Value,
            horizontalAccuracy: location.HorizontalAccuracy,
            verticalAccuracy: location.VerticalAccuracy,
            timestamp: DateTimeOffset.FromUnixTimeSeconds((long)location.Timestamp.SecondsSince1970).DateTime
        );
    }

    internal static NotificareRegion FromNativeRegion(
        NotificareSdk.Geo.iOS.Binding.NotificareRegion region
    )
    {
        return new NotificareRegion(
            id: region.RegionId,
            name: region.Name,
            description: region.RegionDescription,
            referenceKey: region.ReferenceKey,
            geometry: FromNativeRegionGeometry(region.Geometry),
            advancedGeometry: region.AdvancedGeometry == null
                ? null
                : FromNativeRegionAdvancedGeometry(region.AdvancedGeometry),
            major: region.Major == null ? null : region.Major.Int32Value,
            distance: region.Distance,
            timeZone: region.TimeZone,
            timeZoneOffset: region.TimeZoneOffset
        );
    }

    private static NotificareRegionGeometry FromNativeRegionGeometry(
        NotificareSdk.Geo.iOS.Binding.NotificareRegionGeometry geometry
    )
    {
        return new NotificareRegionGeometry(
            type: geometry.Type,
            coordinate: FromNativeRegionCoordinate(geometry.Coordinate)
        );
    }

    private static NotificareRegionAdvancedGeometry FromNativeRegionAdvancedGeometry(
        NotificareSdk.Geo.iOS.Binding.NotificareRegionAdvancedGeometry geometry
    )
    {
        return new NotificareRegionAdvancedGeometry(
            type: geometry.Type,
            coordinates: geometry.Coordinates.Select(FromNativeRegionCoordinate).ToList()
        );
    }

    private static NotificareRegionCoordinate FromNativeRegionCoordinate(
        NotificareSdk.Geo.iOS.Binding.NotificareRegionCoordinate coordinate
    )
    {
        return new NotificareRegionCoordinate(
            latitude: coordinate.Latitude,
            longitude: coordinate.Longitude
        );
    }

    internal static NotificareBeacon FromNativeBeacon(
        NotificareSdk.Geo.iOS.Binding.NotificareBeacon beacon
    )
    {
        return new NotificareBeacon(
            id: beacon.BeaconId,
            name: beacon.Name,
            major: beacon.Major.ToInt32(),
            minor: beacon.Minor == null ? null : beacon.Minor.Int32Value,
            triggers: beacon.Triggers,
            proximity: FromNativeBeaconProximity(beacon.Proximity)
        );
    }

    private static NotificareBeaconProximity FromNativeBeaconProximity(
        NotificareSdk.Geo.iOS.Binding.NotificareBeaconProximity proximity)
    {
        switch (proximity)
        {
            case iOS.Binding.NotificareBeaconProximity.Unknown:
                return NotificareBeaconProximity.Unknown;
            case iOS.Binding.NotificareBeaconProximity.Immediate:
                return NotificareBeaconProximity.Immediate;
            case iOS.Binding.NotificareBeaconProximity.Near:
                return NotificareBeaconProximity.Near;
            case iOS.Binding.NotificareBeaconProximity.Far:
                return NotificareBeaconProximity.Far;
            default:
                throw new ArgumentException($"Unknown beacon proximity: {proximity}");
        }
    }

    internal static NotificareVisit FromNativeVisit(
        NotificareSdk.Geo.iOS.Binding.NotificareVisit visit
    )
    {
        return new NotificareVisit(
            departureDate: DateTimeOffset.FromUnixTimeSeconds((long)visit.DepartureDate.SecondsSince1970).DateTime,
            arrivalDate: DateTimeOffset.FromUnixTimeSeconds((long)visit.ArrivalDate.SecondsSince1970).DateTime,
            latitude: visit.Latitude,
            longitude: visit.Longitude
        );
    }

    internal static NotificareHeading FromNativeHeading(
        NotificareSdk.Geo.iOS.Binding.NotificareHeading heading
    )
    {
        return new NotificareHeading(
            magneticHeading: heading.MagneticHeading,
            trueHeading: heading.TrueHeading,
            headingAccuracy: heading.HeadingAccuracy,
            x: heading.X,
            y: heading.Y,
            z: heading.Z,
            timestamp: DateTimeOffset.FromUnixTimeSeconds((long)heading.Timestamp.SecondsSince1970).DateTime
        );
    }
}

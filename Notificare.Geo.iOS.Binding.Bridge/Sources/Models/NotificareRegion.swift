import Foundation
import NotificareGeoKit

@objc
public class NotificareRegion : NSObject {
    @objc public let regionId: String
    @objc public let name: String
    @objc public let regionDescription: String?
    @objc public let referenceKey: String?
    @objc public let geometry: NotificareRegionGeometry
    @objc public let advancedGeometry: NotificareRegionAdvancedGeometry?
    @objc public let major: NSNumber? // Cannot represent Int? in Objective-C.
    @objc public let distance: Double
    @objc public let timeZone: String
    @objc public let timeZoneOffset: Double

    @objc public init(regionId: String, name: String, regionDescription: String?, referenceKey: String?, geometry: NotificareRegionGeometry, advancedGeometry: NotificareRegionAdvancedGeometry?, major: NSNumber?, distance: Double, timeZone: String, timeZoneOffset: Double) {
        self.regionId = regionId
        self.name = name
        self.regionDescription = regionDescription
        self.referenceKey = referenceKey
        self.geometry = geometry
        self.advancedGeometry = advancedGeometry
        self.major = major
        self.distance = distance
        self.timeZone = timeZone
        self.timeZoneOffset = timeZoneOffset
    }

    public convenience init(from region: NotificareGeoKit.NotificareRegion) {
        self.init(
            regionId: region.id,
            name: region.name,
            regionDescription: region.description,
            referenceKey: region.referenceKey,
            geometry: NotificareRegionGeometry(from: region.geometry),
            advancedGeometry: region.advancedGeometry.map { NotificareRegionAdvancedGeometry(from: $0) },
            major: region.major.map { NSNumber(integerLiteral: $0) },
            distance: region.distance,
            timeZone: region.timeZone,
            timeZoneOffset: Double(region.timeZoneOffset)
        )
    }
}

@objc
public class NotificareRegionGeometry : NSObject {
    @objc public let type: String
    @objc public let coordinate: NotificareRegionCoordinate

    @objc public init(type: String, coordinate: NotificareRegionCoordinate) {
        self.type = type
        self.coordinate = coordinate
    }

    public convenience init(from geometry: NotificareGeoKit.NotificareRegion.Geometry) {
        self.init(
            type: geometry.type,
            coordinate: NotificareRegionCoordinate(from: geometry.coordinate)
        )
    }
}

@objc
public class NotificareRegionAdvancedGeometry : NSObject {
    @objc public let type: String
    @objc public let coordinates: [NotificareRegionCoordinate]

    @objc public init(type: String, coordinates: [NotificareRegionCoordinate]) {
        self.type = type
        self.coordinates = coordinates
    }

    public convenience init(from advancedGeometry: NotificareGeoKit.NotificareRegion.AdvancedGeometry) {
        self.init(
            type: advancedGeometry.type,
            coordinates: advancedGeometry.coordinates.map { NotificareRegionCoordinate(from: $0) }
        )
    }
}

@objc
public class NotificareRegionCoordinate : NSObject {
    @objc public let latitude: Double
    @objc public let longitude: Double

    @objc public init(latitude: Double, longitude: Double) {
        self.latitude = latitude
        self.longitude = longitude
    }

    public convenience init(from coordinate: NotificareGeoKit.NotificareRegion.Coordinate) {
        self.init(
            latitude: coordinate.latitude,
            longitude: coordinate.longitude
        )
    }
}

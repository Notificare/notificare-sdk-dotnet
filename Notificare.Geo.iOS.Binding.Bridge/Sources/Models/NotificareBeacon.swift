import Foundation
import NotificareGeoKit

@objc
public class NotificareBeacon : NSObject {
    @objc public let beaconId: String
    @objc public let name: String
    @objc public let major: Int
    @objc public let minor: NSNumber? // Cannot represent Int? in Objective-C.
    @objc public let triggers: Bool
    @objc public let proximity: NotificareBeaconProximity

    @objc public init(beaconId: String, name: String, major: Int, minor: NSNumber?, triggers: Bool, proximity: NotificareBeaconProximity) {
        self.beaconId = beaconId
        self.name = name
        self.major = major
        self.minor = minor
        self.triggers = triggers
        self.proximity = proximity
    }

    public convenience init(from beacon: NotificareGeoKit.NotificareBeacon) {
        self.init(
            beaconId: beacon.id,
            name: beacon.name,
            major: beacon.major,
            minor: beacon.minor.map { NSNumber(integerLiteral: $0) },
            triggers: beacon.triggers,
            proximity: NotificareBeaconProximity(from: beacon.proximity)
        )
    }
}

@objc
public enum NotificareBeaconProximity : Int {
    case unknown
    case immediate
    case near
    case far

    init(from proximity: NotificareGeoKit.NotificareBeacon.Proximity) {
        switch proximity {
        case .unknown:
            self = .unknown
        case .immediate:
            self = .immediate
        case .near:
            self = .near
        case .far:
            self = .far
        @unknown default:
            fatalError("Unknown beacon proximity: \(proximity.rawValue)")
        }
    }
}

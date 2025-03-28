import Foundation
import NotificareGeoKit

@objc
public class NotificareVisit : NSObject {
    @objc public let departureDate: Date
    @objc public let arrivalDate: Date
    @objc public let latitude: Double
    @objc public let longitude: Double

    @objc public init(departureDate: Date, arrivalDate: Date, latitude: Double, longitude: Double) {
        self.departureDate = departureDate
        self.arrivalDate = arrivalDate
        self.latitude = latitude
        self.longitude = longitude
    }

    public convenience init(from visit: NotificareGeoKit.NotificareVisit) {
        self.init(
            departureDate: visit.departureDate,
            arrivalDate: visit.arrivalDate,
            latitude: visit.latitude,
            longitude: visit.longitude
        )
    }
}

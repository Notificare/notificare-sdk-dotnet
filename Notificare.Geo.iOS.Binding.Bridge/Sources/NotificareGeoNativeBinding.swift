import CoreLocation
import Foundation
import NotificareKit
import NotificareGeoKit

@objc(NotificareGeoNativeBinding)
public class NotificareGeoNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.geo().delegate = self
    }

    @objc
    public weak var delegate: NotificareGeoNativeBindingDelegate?


    @objc
    public var hasLocationServicesEnabled: Bool {
        Notificare.shared.geo().hasLocationServicesEnabled
    }

    @objc
    public var hasBluetoothEnabled: Bool {
        Notificare.shared.geo().hasBluetoothEnabled
    }

    @objc
    public var monitoredRegions: [NotificareRegion] {
        Notificare.shared.geo().monitoredRegions.map {
            NotificareRegion(from: $0)
        }
    }

    @objc
    public var enteredRegions: [NotificareRegion] {
        Notificare.shared.geo().enteredRegions.map {
            NotificareRegion(from: $0)
        }
    }

    @objc
    public func enableLocationUpdates() {
        Notificare.shared.geo().enableLocationUpdates()
    }

    @objc
    public func disableLocationUpdates() {
        Notificare.shared.geo().disableLocationUpdates()
    }
}

extension NotificareGeoNativeBinding : NotificareGeoDelegate {
    public func notificare(_ notificareGeo: any NotificareGeo, didUpdateLocations locations: [NotificareGeoKit.NotificareLocation]) {
        delegate?.notificare(self, didUpdateLocations: locations.map { NotificareLocation(from: $0) })
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didFailWith error: any Error) {
        delegate?.notificare(self, didFailWith: error)
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didStartMonitoringFor region: NotificareGeoKit.NotificareRegion) {
        delegate?.notificare(self, didStartMonitoringForRegion: NotificareRegion(from: region))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didStartMonitoringFor beacon: NotificareGeoKit.NotificareBeacon) {
        delegate?.notificare(self, didStartMonitoringForBeacon: NotificareBeacon(from: beacon))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, monitoringDidFailFor region: NotificareGeoKit.NotificareRegion, with error: any Error) {
        delegate?.notificare(self, monitoringDidFailForRegion: NotificareRegion(from: region), with: error)
    }

    public func notificare(_ notificareGeo: any NotificareGeo, monitoringDidFailFor beacon: NotificareGeoKit.NotificareBeacon, with error: any Error) {
        delegate?.notificare(self, monitoringDidFailForBeacon: NotificareBeacon(from: beacon), with: error)
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didDetermineState state: CLRegionState, for region: NotificareGeoKit.NotificareRegion) {
        delegate?.notificare(self, didDetermineState: state, forRegion: NotificareRegion(from: region))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didDetermineState state: CLRegionState, for beacon: NotificareGeoKit.NotificareBeacon) {
        delegate?.notificare(self, didDetermineState: state, forBeacon: NotificareBeacon(from: beacon))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didEnter region: NotificareGeoKit.NotificareRegion) {
        delegate?.notificare(self, didEnterRegion: NotificareRegion(from: region))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didEnter beacon: NotificareGeoKit.NotificareBeacon) {
        delegate?.notificare(self, didEnterBeacon: NotificareBeacon(from: beacon))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didExit region: NotificareGeoKit.NotificareRegion) {
        delegate?.notificare(self, didExitRegion: NotificareRegion(from: region))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didExit beacon: NotificareGeoKit.NotificareBeacon) {
        delegate?.notificare(self, didExitBeacon: NotificareBeacon(from: beacon))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didVisit visit: NotificareGeoKit.NotificareVisit) {
        delegate?.notificare(self, didVisit: NotificareVisit(from: visit))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didUpdateHeading heading: NotificareGeoKit.NotificareHeading) {
        delegate?.notificare(self, didUpdateHeading: NotificareHeading(from: heading))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didRange beacons: [NotificareGeoKit.NotificareBeacon], in region: NotificareGeoKit.NotificareRegion) {
        delegate?.notificare(self, didRange: beacons.map { NotificareBeacon(from: $0) }, in: NotificareRegion(from: region))
    }

    public func notificare(_ notificareGeo: any NotificareGeo, didFailRangingFor region: NotificareGeoKit.NotificareRegion, with error: any Error) {
        delegate?.notificare(self, didFailRangingFor: NotificareRegion(from: region), with: error)
    }
}

@objc
public protocol NotificareGeoNativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didUpdateLocations locations: [NotificareLocation])

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didFailWith error: Error)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didStartMonitoringForRegion region: NotificareRegion)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didStartMonitoringForBeacon beacon: NotificareBeacon)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, monitoringDidFailForRegion region: NotificareRegion, with error: Error)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, monitoringDidFailForBeacon beacon: NotificareBeacon, with error: Error)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didDetermineState state: CLRegionState, forRegion region: NotificareRegion)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didDetermineState state: CLRegionState, forBeacon beacon: NotificareBeacon)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didEnterRegion region: NotificareRegion)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didEnterBeacon beacon: NotificareBeacon)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didExitRegion region: NotificareRegion)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didExitBeacon beacon: NotificareBeacon)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didVisit visit: NotificareVisit)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didUpdateHeading heading: NotificareHeading)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didRange beacons: [NotificareBeacon], in region: NotificareRegion)

    func notificare(_ notificareGeo: NotificareGeoNativeBinding, didFailRangingFor region: NotificareRegion, with error: Error)
}

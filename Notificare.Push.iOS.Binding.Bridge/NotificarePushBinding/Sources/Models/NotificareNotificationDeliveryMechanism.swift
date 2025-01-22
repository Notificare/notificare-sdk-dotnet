import Foundation
import NotificarePushKit

@objc
public enum NotificareNotificationDeliveryMechanism : Int {
    case standard
    case silent

    init(from deliveryMechanism: NotificarePushKit.NotificareNotificationDeliveryMechanism) {
        switch deliveryMechanism {
        case .standard:
            self = .standard
        case .silent:
            self = .silent
        @unknown default:
            fatalError("Unknown delivery mechanism: \(deliveryMechanism.rawValue)")
        }
    }
}

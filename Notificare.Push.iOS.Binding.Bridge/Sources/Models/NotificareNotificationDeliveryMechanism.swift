import Foundation
import NotificarePushKit

@objc
public enum NotificareNotificationDeliveryMechanism : Int {
    case standard
    case silent

    // Objective-Sharpie uses the Objective-C, but removes the common prefix in enum cases.
    // Without this case, 'NotificareNotificationDeliveryMechanismStandard' would become 'tandard'.
    case unknown

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

import Foundation
import NotificarePushKit

@objc
public enum NotificareTransport: Int {
    case notificare
    case apns
    case unknown

    public init(from transport: NotificarePushKit.NotificareTransport?) {
        switch transport {
        case .notificare:
            self = .notificare
        case .apns:
            self = .apns
        case .none:
            self = .unknown
        @unknown default:
            self = .unknown
        }
    }
}

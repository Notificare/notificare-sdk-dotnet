import Foundation
import NotificareScannablesKit
import NotificareBinding

@objc
public class NotificareScannable : NSObject {
    @objc public let scannableId: String
    @objc public let name: String
    @objc public let tag: String
    @objc public let type: String
    @objc public let notification: NotificareNotification?

    @objc public init(scannableId: String, name: String, tag: String, type: String, notification: NotificareNotification?) {
        self.scannableId = scannableId
        self.name = name
        self.tag = tag
        self.type = type
        self.notification = notification
    }

    public convenience init(from scannable: NotificareScannablesKit.NotificareScannable) {
        self.init(
            scannableId: scannable.id,
            name: scannable.name,
            tag: scannable.tag,
            type: scannable.type,
            notification: scannable.notification.map { NotificareNotification(from: $0) }
        )
    }
}

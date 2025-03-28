import Foundation
import NotificareUserInboxKit
import NotificareBinding

@objc
public class NotificareUserInboxItem : NSObject {
    @objc public let inboxItemId: String
    @objc public let notification: NotificareNotification
    @objc public let time: Date
    @objc public let opened: Bool
    @objc public let expires: Date?

    @objc public init(inboxItemId: String, notification: NotificareNotification, time: Date, opened: Bool, expires: Date?) {
        self.inboxItemId = inboxItemId
        self.notification = notification
        self.time = time
        self.opened = opened
        self.expires = expires
    }

    public convenience init(from item: NotificareUserInboxKit.NotificareUserInboxItem) {
        self.init(
            inboxItemId: item.id,
            notification: NotificareBinding.NotificareNotification(from: item.notification),
            time: item.time,
            opened: item.opened,
            expires: item.expires
        )
    }

    public func toNative() -> NotificareUserInboxKit.NotificareUserInboxItem {
        return NotificareUserInboxKit.NotificareUserInboxItem(
            id: inboxItemId,
            notification: notification.toNative(),
            time: time,
            opened: opened,
            expires: expires
        )
    }
}

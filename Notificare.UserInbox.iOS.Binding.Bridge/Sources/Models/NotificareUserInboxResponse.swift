import Foundation
import NotificareUserInboxKit

@objc
public class NotificareUserInboxResponse : NSObject {
    @objc public let count: Int
    @objc public let unread: Int
    @objc public let items: [NotificareUserInboxItem]

    @objc public init(count: Int, unread: Int, items: [NotificareUserInboxItem]) {
        self.count = count
        self.unread = unread
        self.items = items
    }

    public convenience init(from response: NotificareUserInboxKit.NotificareUserInboxResponse) {
        self.init(
            count: response.count,
            unread: response.unread,
            items: response.items.map { NotificareUserInboxItem(from: $0) }
        )
    }
}

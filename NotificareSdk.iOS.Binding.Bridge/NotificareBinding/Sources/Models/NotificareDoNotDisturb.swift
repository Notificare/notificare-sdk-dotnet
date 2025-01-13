import Foundation
import NotificareKit

@objc
public class NotificareDoNotDisturb : NSObject {
    @objc public let start: NotificareTime
    @objc public let end: NotificareTime

    @objc public init(start: NotificareTime, end: NotificareTime) {
        self.start = start
        self.end = end
    }

    public convenience init(from dnd: NotificareKit.NotificareDoNotDisturb) {
        self.init(
            start: NotificareTime(from: dnd.start),
            end: NotificareTime(from: dnd.end)
        )
    }

    public func toNative() throws -> NotificareKit.NotificareDoNotDisturb {
        return NotificareKit.NotificareDoNotDisturb(
            start: try NotificareKit.NotificareTime(
                hours: start.hours,
                minutes: start.minutes
            ),
            end: try NotificareKit.NotificareTime(
                hours: end.hours,
                minutes: end.minutes
            )
        )
    }
}

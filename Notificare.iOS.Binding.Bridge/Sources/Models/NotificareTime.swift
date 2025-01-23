import Foundation
import NotificareKit

@objc
public class NotificareTime : NSObject {
    @objc public let hours: Int
    @objc public let minutes: Int

    @objc public init(hours: Int, minutes: Int) {
        self.hours = hours
        self.minutes = minutes
    }

    public convenience init(from time: NotificareKit.NotificareTime) {
        self.init(
            hours: time.hours,
            minutes: time.minutes
        )
    }
}

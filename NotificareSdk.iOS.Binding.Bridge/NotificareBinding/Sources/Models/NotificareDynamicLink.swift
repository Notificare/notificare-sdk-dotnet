import Foundation
import NotificareKit

@objc
public class NotificareDynamicLink : NSObject {
    @objc public let target: String

    @objc public init(target: String) {
        self.target = target
    }

    public convenience init(from link: NotificareKit.NotificareDynamicLink) {
        self.init(target: link.target)
    }
}

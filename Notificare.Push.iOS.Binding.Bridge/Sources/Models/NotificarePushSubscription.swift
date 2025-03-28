import Foundation
import NotificarePushKit

@objc
public class NotificarePushSubscription : NSObject {
    @objc public let token: String

    @objc public init(token: String) {
        self.token = token
    }

    public convenience init(from subscription: NotificarePushKit.NotificarePushSubscription) {
        self.init(
            token: subscription.token
        )
    }
}

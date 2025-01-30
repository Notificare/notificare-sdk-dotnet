import Foundation
import NotificareKit
import NotificareInAppMessagingKit

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificareInAppMessagingNativeBinding)
public class NotificareInAppMessagingNativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.inAppMessaging().delegate = self
    }

    @objc
    public weak var delegate: NotificareInAppMessagingNativeBindingDelegate?

    @objc
    public var hasMessagesSuppressed: Bool {
        get { Notificare.shared.inAppMessaging().hasMessagesSuppressed }
        set { Notificare.shared.inAppMessaging().hasMessagesSuppressed = newValue }
    }

    @objc public func setMessagesSuppressed(_ suppressed: Bool, evaluateContext: Bool) {
        Notificare.shared.inAppMessaging().setMessagesSuppressed(suppressed, evaluateContext: evaluateContext)
    }
}

extension NotificareInAppMessagingNativeBinding : NotificareInAppMessagingDelegate {
    public func notificare(_ notificare: any NotificareInAppMessaging, didPresentMessage message: NotificareInAppMessagingKit.NotificareInAppMessage) {
        delegate?.notificare(self, didPresentMessage: NotificareInAppMessage(from: message))
    }

    public func notificare(_ notificare: any NotificareInAppMessaging, didFinishPresentingMessage message: NotificareInAppMessagingKit.NotificareInAppMessage) {
        delegate?.notificare(self, didFinishPresentingMessage: NotificareInAppMessage(from: message))
    }

    public func notificare(_ notificare: any NotificareInAppMessaging, didFailToPresentMessage message: NotificareInAppMessagingKit.NotificareInAppMessage) {
        delegate?.notificare(self, didFailToPresentMessage: NotificareInAppMessage(from: message))
    }

    public func notificare(_ notificare: any NotificareInAppMessaging, didExecuteAction action: NotificareInAppMessagingKit.NotificareInAppMessage.Action, for message: NotificareInAppMessagingKit.NotificareInAppMessage) {
        delegate?.notificare(self, didExecuteAction: NotificareInAppMessageAction(from: action), for: NotificareInAppMessage(from: message))
    }

    public func notificare(_ notificare: any NotificareInAppMessaging, didFailToExecuteAction action: NotificareInAppMessagingKit.NotificareInAppMessage.Action, for message: NotificareInAppMessagingKit.NotificareInAppMessage, error: (any Error)?) {
        delegate?.notificare(self, didFailToExecuteAction: NotificareInAppMessageAction(from: action), for: NotificareInAppMessage(from: message), error: error)
    }
}

@objc
public protocol NotificareInAppMessagingNativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificare: NotificareInAppMessagingNativeBinding, didPresentMessage message: NotificareInAppMessage)

    func notificare(_ notificare: NotificareInAppMessagingNativeBinding, didFinishPresentingMessage message: NotificareInAppMessage)

    func notificare(_ notificare: NotificareInAppMessagingNativeBinding, didFailToPresentMessage message: NotificareInAppMessage)

    func notificare(_ notificare: NotificareInAppMessagingNativeBinding, didExecuteAction action: NotificareInAppMessageAction, for message: NotificareInAppMessage)

    func notificare(_ notificare: NotificareInAppMessagingNativeBinding, didFailToExecuteAction action: NotificareInAppMessageAction, for message: NotificareInAppMessage, error: Error?)
}

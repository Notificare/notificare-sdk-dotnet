import Foundation
import UIKit
import NotificareKit
import NotificarePushUIKit
import NotificareBinding

public typealias SuccessBlock<T> = (T) -> Void
public typealias VoidBlock = () -> Void
public typealias ErrorBlock = (Error) -> Void

@objc(NotificarePushUINativeBinding)
public class NotificarePushUINativeBinding : NSObject {

    public override init() {
        super.init()

        Notificare.shared.pushUI().delegate = self
    }

    @objc
    public weak var delegate: NotificarePushUINativeBindingDelegate?

    @objc
    public func presentNotification(_ notification: NotificareBinding.NotificareNotification, in controller: UIViewController) {
        Notificare.shared.pushUI().presentNotification(notification.toNative(), in: controller)
    }

    @objc
    public func presentAction(_ action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification, in controller: UIViewController) {
        Notificare.shared.pushUI().presentAction(action.toNative(), for: notification.toNative(), in: controller)
    }
}

extension NotificarePushUINativeBinding : NotificarePushUIDelegate {
    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, willPresentNotification notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, willPresentNotification: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didPresentNotification notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didPresentNotification: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didFinishPresentingNotification notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didFinishPresentingNotification: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didFailToPresentNotification notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didFailToPresentNotification: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didClickURL url: URL, in notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didClickURL: url, in: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, willExecuteAction action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, willExecuteAction: NotificareNotificationAction(from: action), for: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didExecuteAction action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didExecuteAction: NotificareNotificationAction(from: action), for: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didNotExecuteAction action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didNotExecuteAction: NotificareNotificationAction(from: action), for: NotificareNotification(from: notification))
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didFailToExecuteAction action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification, error: (any Error)?) {
        delegate?.notificare(self, didFailToExecuteAction: NotificareNotificationAction(from: action), for: NotificareNotification(from: notification), error: error)
    }

    public func notificare(_ notificarePushUI: any NotificarePushUIKit.NotificarePushUI, didReceiveCustomAction url: URL, in action: NotificareKit.NotificareNotification.Action, for notification: NotificareKit.NotificareNotification) {
        delegate?.notificare(self, didReceiveCustomAction: url, in: NotificareNotificationAction(from: action), for: NotificareNotification(from: notification))
    }
}

@objc
public protocol NotificarePushUINativeBindingDelegate : NSObjectProtocol {
    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, willPresentNotification notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didPresentNotification notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didFinishPresentingNotification notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didFailToPresentNotification notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didClickURL url: URL, in notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, willExecuteAction action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didExecuteAction action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didNotExecuteAction action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didFailToExecuteAction action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification, error: Error?)

    func notificare(_ notificarePushUI: NotificarePushUINativeBinding, didReceiveCustomAction url: URL, in action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification)
}

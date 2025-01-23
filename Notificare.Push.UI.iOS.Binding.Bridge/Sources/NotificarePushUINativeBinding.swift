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

//        Notificare.shared.pushUI().delegate = self
    }

    @objc
    public func presentNotification(_ notification: NotificareBinding.NotificareNotification, in controller: UIViewController) {
        Notificare.shared.pushUI().presentNotification(notification.toNative(), in: controller)
    }

    @objc
    public func presentAction(_ action: NotificareBinding.NotificareNotificationAction, for notification: NotificareBinding.NotificareNotification, in controller: UIViewController) {
        Notificare.shared.pushUI().presentAction(action.toNative(), for: notification.toNative(), in: controller)
    }
}

//extension NotificarePushUINativeBinding : NotificarePushUIDelegate {
//
//}

@objc
public protocol NotificarePushUINativeBindingDelegate : NSObjectProtocol {

}
